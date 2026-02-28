#!/usr/bin/env bash
set -euo pipefail

# KVM detection — fall back to software rendering if unavailable
if [ -e /dev/kvm ] && [ -r /dev/kvm ] && [ -w /dev/kvm ]; then
    echo "[entrypoint] KVM available — using hardware acceleration"
    ACCEL_FLAG="-accel kvm"
else
    echo "[entrypoint] KVM not available — falling back to -no-accel (slow but functional)"
    ACCEL_FLAG="-no-accel"
fi

# Start emulator in background
$ANDROID_HOME/emulator/emulator \
    -avd Pixel_6_API_34 \
    -no-window -no-audio -no-boot-anim \
    -gpu swiftshader_indirect \
    $ACCEL_FLAG &

EMULATOR_PID=$!

# Wait for full boot (sys.boot_completed=1)
echo "[entrypoint] Waiting for emulator to boot..."
ELAPSED=0; TIMEOUT=300; INTERVAL=5
while true; do
    STATUS=$(adb shell getprop sys.boot_completed 2>/dev/null | tr -d '\r' || true)
    [ "$STATUS" = "1" ] && { echo "[entrypoint] Booted after ${ELAPSED}s"; break; }
    [ "$ELAPSED" -ge "$TIMEOUT" ] && { echo "[entrypoint] ERROR: boot timeout"; kill "$EMULATOR_PID" 2>/dev/null; exit 1; }
    sleep "$INTERVAL"; ELAPSED=$(( ELAPSED + INTERVAL ))
done

# Disable animations for stable UiAutomator2 tests
adb shell settings put global window_animation_scale 0.0
adb shell settings put global transition_animation_scale 0.0
adb shell settings put global animator_duration_scale 0.0

dotnet test "AppiumTests/AppiumTests.csproj" --no-build -c Release "$@" && TEST_EXIT=0 || TEST_EXIT=$?

echo "[entrypoint] Generating Allure report..."
allure generate --single-file allure-results -o allure-report || true

exit $TEST_EXIT
