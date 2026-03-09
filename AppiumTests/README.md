# AppiumTests (Appium mobile tests)

NUnit test project for mobile UI testing using [Appium](https://appium.io/). Includes an Appium driver factory, iOS/Android configuration helpers, and a base page object.

## macOS Setup

1. **Install Homebrew** (if not installed):

   ```bash
   /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
   ```

2. **Install .NET 8 SDK:**

   ```bash
   brew install --cask dotnet-sdk
   dotnet --version   # should show 8.x.x
   ```

3. **Install JDK 21:**

   ```bash
   brew install openjdk@21
   sudo ln -sfn $(brew --prefix openjdk@21)/libexec/openjdk.jdk /Library/Java/JavaVirtualMachines/openjdk-21.jdk
   ```

   Add to `~/.zshrc`:

   ```bash
   export JAVA_HOME=$(/usr/libexec/java_home -v 21)
   export PATH=$JAVA_HOME/bin:$PATH
   ```

   Reload and verify:

   ```bash
   source ~/.zshrc
   java -version   # must show openjdk 21
   ```

4. **Install Android SDK command-line tools:**

   Download from https://developer.android.com/studio#command-tools, then:

   ```bash
   mkdir -p ~/android-sdk/cmdline-tools
   unzip commandlinetools-mac-*.zip -d ~/android-sdk/cmdline-tools
   mv ~/android-sdk/cmdline-tools/cmdline-tools ~/android-sdk/cmdline-tools/latest
   ```

   Add to `~/.zshrc`:

   ```bash
   export ANDROID_HOME=~/android-sdk
   export ANDROID_SDK_ROOT=$ANDROID_HOME
   export PATH=$ANDROID_HOME/cmdline-tools/latest/bin:$ANDROID_HOME/platform-tools:$ANDROID_HOME/emulator:$PATH
   ```

   Reload: `source ~/.zshrc`

5. **Accept licenses and install SDK components:**

   ```bash
   yes | sdkmanager --licenses
   sdkmanager "platform-tools" "platforms;android-34" "build-tools;34.0.0" \
              "system-images;android-34;google_apis;x86_64" "emulator"
   ```

6. **Create the AVD:**

   ```bash
   avdmanager create avd \
     -n Pixel_6_API_34 \
     -k "system-images;android-34;google_apis;x86_64" \
     -d "pixel_6"
   avdmanager list avd   # verify it appears
   ```

7. **Start the Android emulator:**

   ```bash
   emulator -avd Pixel_6_API_34 -no-snapshot &
   adb wait-for-device
   adb devices   # should list the emulator
   ```

8. **Install Xcode (iOS only):**

   Install Xcode from the Mac App Store, then:

   ```bash
   sudo xcodebuild -license accept
   ```

   Boot the iOS simulator and verify:

   ```bash
   xcrun simctl boot "iPhone 16e"
   xcrun simctl list devices | grep "iPhone 16e"
   ```

9. **Install Node.js 20:**

   ```bash
   brew install node@20
   node -v   # should show v20.x.x
   npm -v
   ```

10. **Install Appium and drivers:**

    ```bash
    npm install -g appium
    appium driver install uiautomator2   # Android
    appium driver install xcuitest       # iOS only
    appium driver list --installed
    ```

    Run the doctor to verify your environment:

    ```bash
    npm install -g appium-doctor
    appium-doctor --android
    appium-doctor --ios   # iOS only
    ```

11. **Clone and build the project:**

    ```bash
    git clone <repo-url> && cd Accelerator
    dotnet restore AppiumTests/AppiumTests.csproj
    dotnet build AppiumTests/AppiumTests.csproj
    ```

12. **Set environment variables** — add to `~/.zshrc`:

    ```bash
    export EXECUTION_MODE=Local
    export TARGET_PLATFORM=Android
    export TEST_TYPE=Browser
    ```

    For BrowserStack (optional):

    ```bash
    export BrowserStack__UserName=<your-username>
    export BrowserStack__AccessKey=<your-access-key>
    ```

    Reload: `source ~/.zshrc`

13. **(Optional) Install Allure CLI:**

    ```bash
    brew install allure
    ```

14. **Start the Appium server** in a separate terminal:

    ```bash
    appium --address 127.0.0.1 --port 4723
    ```

15. **Run tests:**

    ```bash
    # Android browser (default)
    dotnet test AppiumTests/AppiumTests.csproj

    # iOS browser (requires Xcode)
    TARGET_PLATFORM=iOS dotnet test AppiumTests/AppiumTests.csproj

    # Android native app
    APP_PATH=/path/to/app.apk TEST_TYPE=NativeApp dotnet test AppiumTests/AppiumTests.csproj
    ```

## Windows 11 Setup

> **Note:** iOS simulator testing is macOS-only. Windows supports Android emulator and BrowserStack testing.

1. **Verify winget** is available (comes with App Installer from the Microsoft Store):

   ```powershell
   winget --version
   ```

2. **Install .NET 8 SDK:**

   ```powershell
   winget install Microsoft.DotNet.SDK.8
   ```

3. **Install JDK 21:**

   ```powershell
   winget install EclipseAdoptium.Temurin.21.JDK
   ```

   Set `JAVA_HOME` and update `PATH`:

   ```powershell
   $javaHome = "C:\Program Files\Eclipse Adoptium\jdk-21*"
   [System.Environment]::SetEnvironmentVariable("JAVA_HOME", (Resolve-Path $javaHome).Path, "User")
   $path = [System.Environment]::GetEnvironmentVariable("PATH", "User")
   [System.Environment]::SetEnvironmentVariable("PATH", "$path;$env:JAVA_HOME\bin", "User")
   ```

   Restart the terminal, then verify:

   ```powershell
   java -version   # must show openjdk 21
   ```

4. **Install Android SDK command-line tools:**

   Download from https://developer.android.com/studio#command-tools, then in PowerShell:

   ```powershell
   New-Item -ItemType Directory -Path "$env:USERPROFILE\android-sdk\cmdline-tools" -Force
   Expand-Archive commandlinetools-win-*.zip -DestinationPath "$env:USERPROFILE\android-sdk\cmdline-tools"
   Rename-Item "$env:USERPROFILE\android-sdk\cmdline-tools\cmdline-tools" "latest"
   ```

   Set environment variables:

   ```powershell
   [System.Environment]::SetEnvironmentVariable("ANDROID_HOME", "$env:USERPROFILE\android-sdk", "User")
   [System.Environment]::SetEnvironmentVariable("ANDROID_SDK_ROOT", "$env:USERPROFILE\android-sdk", "User")
   $path = [System.Environment]::GetEnvironmentVariable("PATH", "User")
   [System.Environment]::SetEnvironmentVariable("PATH",
     "$path;$env:USERPROFILE\android-sdk\cmdline-tools\latest\bin;$env:USERPROFILE\android-sdk\platform-tools;$env:USERPROFILE\android-sdk\emulator",
     "User")
   ```

   Restart the terminal for variables to take effect.

5. **Accept licenses and install SDK components:**

   ```powershell
   # Answer 'y' to each license prompt
   sdkmanager --licenses
   sdkmanager "platform-tools" "platforms;android-34" "build-tools;34.0.0" `
              "system-images;android-34;google_apis;x86_64" "emulator"
   ```

6. **Create the AVD:**

   ```powershell
   avdmanager create avd `
     -n Pixel_6_API_34 `
     -k "system-images;android-34;google_apis;x86_64" `
     -d "pixel_6"
   avdmanager list avd   # verify it appears
   ```

   > **Note:** For hardware acceleration, ensure Hyper-V is enabled (`optionalfeatures.exe` → Hyper-V).

7. **Start the Android emulator:**

   ```powershell
   Start-Process emulator -ArgumentList "-avd Pixel_6_API_34 -no-snapshot"
   adb wait-for-device
   adb devices   # should list the emulator
   ```

8. **Install Node.js 20:**

   ```powershell
   winget install OpenJS.NodeJS.LTS
   node -v   # should show v20.x.x
   ```

   Restart the terminal for `node`/`npm` to be on the path.

9. **Install Appium and UIAutomator2 driver:**

   ```powershell
   npm install -g appium
   appium driver install uiautomator2
   appium driver list --installed
   ```

   Verify your environment:

   ```powershell
   npm install -g appium-doctor
   appium-doctor --android
   ```

10. **Clone and build the project:**

    ```powershell
    git clone <repo-url> && cd Accelerator
    dotnet restore AppiumTests/AppiumTests.csproj
    dotnet build AppiumTests/AppiumTests.csproj
    ```

11. **Set environment variables:**

    ```powershell
    [System.Environment]::SetEnvironmentVariable("EXECUTION_MODE", "Local", "User")
    [System.Environment]::SetEnvironmentVariable("TARGET_PLATFORM", "Android", "User")
    [System.Environment]::SetEnvironmentVariable("TEST_TYPE", "Browser", "User")
    ```

    For BrowserStack (optional):

    ```powershell
    [System.Environment]::SetEnvironmentVariable("BrowserStack__UserName", "<your-username>", "User")
    [System.Environment]::SetEnvironmentVariable("BrowserStack__AccessKey", "<your-access-key>", "User")
    ```

    Restart the terminal for variables to take effect.

12. **(Optional) Install Allure CLI:**

    ```powershell
    npm install -g allure-commandline
    ```

13. **Start the Appium server** in a separate terminal:

    ```powershell
    appium --address 127.0.0.1 --port 4723
    ```

14. **Run tests:**

    ```powershell
    # Android browser (default)
    dotnet test AppiumTests/AppiumTests.csproj

    # Android native app
    $env:APP_PATH = "C:\path\to\app.apk"
    $env:TEST_TYPE = "NativeApp"
    dotnet test AppiumTests/AppiumTests.csproj
    ```

## Configuration

Edit `AppiumTests/appsettings.json` (or create `appsettings.Test.json` to override without modifying the committed file):

```json
{
  "TestEnvironment": {
    "BaseUrl": "https://example.com",
    "ApiUri": "https://api.example.com",
    "Username": "",
    "Password": ""
  },
  "BrowserStack": {
    "UserName": "",
    "AccessKey": "",
    "AndroidDeviceName": "Samsung Galaxy S23",
    "AndroidOsVersion": "13.0",
    "AndroidBrowserName": "chrome",
    "AndroidAppUrl": "",
    "IosDeviceName": "iPhone 14",
    "IosOsVersion": "16",
    "IosBrowserName": "safari",
    "IosAppUrl": "",
    "ProjectName": "Accelerator",
    "BuildName": "Local Build",
    "SessionName": "AppiumTests"
  }
}
```

### Environment variables

Three env vars control where and how tests run:

| Variable | Values | Default | Description |
|---|---|---|---|
| `EXECUTION_MODE` | `Local`, `BrowserStack` | `Local` | Run against local Appium server or BrowserStack cloud |
| `TARGET_PLATFORM` | `Android`, `iOS` | `Android` | Target mobile platform |
| `TEST_TYPE` | `Browser`, `NativeApp` | `Browser` | Browser automation or native app testing |

BrowserStack credentials and app URLs must be injected via env vars (never commit them):

| Env var | Description |
|---|---|
| `BrowserStack__UserName` | BrowserStack username |
| `BrowserStack__AccessKey` | BrowserStack access key |
| `BrowserStack__AndroidAppUrl` | `bs://...` URL from BrowserStack app upload (Android native) |
| `BrowserStack__IosAppUrl` | `bs://...` URL from BrowserStack app upload (iOS native) |

## Running tests locally

From the repository root:

```bash
# Android browser (default)
dotnet test AppiumTests/AppiumTests.csproj

# iOS browser (macOS + Xcode required)
TARGET_PLATFORM=iOS dotnet test AppiumTests/AppiumTests.csproj
```

### Native App Testing

Set `TEST_TYPE=NativeApp` and provide the path to your `.apk` or `.ipa`:

```bash
# Android native app
APP_PATH=/path/to/app.apk TEST_TYPE=NativeApp \
  dotnet test AppiumTests/AppiumTests.csproj

# iOS native app (macOS + Xcode required)
APP_PATH=/path/to/app.ipa TEST_TYPE=NativeApp TARGET_PLATFORM=iOS \
  dotnet test AppiumTests/AppiumTests.csproj
```

Options:

```bash
# Verbose output
dotnet test AppiumTests --logger "console;verbosity=detailed"

# Run a single test class
dotnet test AppiumTests --filter "FullyQualifiedName~AndroidBrowserTests"
```

## Running on BrowserStack

Set `EXECUTION_MODE=BrowserStack` along with your credentials. No local Appium server or emulator is needed.

### Browser tests

```bash
# Android browser
EXECUTION_MODE=BrowserStack TARGET_PLATFORM=Android \
  BrowserStack__UserName=user BrowserStack__AccessKey=key \
  dotnet test AppiumTests/AppiumTests.csproj

# iOS browser
EXECUTION_MODE=BrowserStack TARGET_PLATFORM=iOS \
  BrowserStack__UserName=user BrowserStack__AccessKey=key \
  dotnet test AppiumTests/AppiumTests.csproj
```

### Native app tests

First upload your app to BrowserStack and get a `bs://` URL:

```bash
curl -u "user:key" \
  -X POST "https://api-cloud.browserstack.com/app-automate/upload" \
  -F "file=@/path/to/app.apk"
# Returns: { "app_url": "bs://abc123..." }
```

Then run:

```bash
# Android native app
EXECUTION_MODE=BrowserStack TARGET_PLATFORM=Android TEST_TYPE=NativeApp \
  BrowserStack__UserName=user BrowserStack__AccessKey=key \
  BrowserStack__AndroidAppUrl=bs://abc123 \
  dotnet test AppiumTests/AppiumTests.csproj

# iOS native app
EXECUTION_MODE=BrowserStack TARGET_PLATFORM=iOS TEST_TYPE=NativeApp \
  BrowserStack__UserName=user BrowserStack__AccessKey=key \
  BrowserStack__IosAppUrl=bs://xyz456 \
  dotnet test AppiumTests/AppiumTests.csproj
```

## Running with Docker

From the repository root:

```bash
docker build -f AppiumTests/Dockerfile -t appium-tests .
docker run --rm appium-tests
```

Or use Docker Compose:

```bash
docker compose build appium-tests
docker compose up appium-tests
```

> **Note:** The Docker container runs the Android emulator internally via KVM (requires `--device /dev/kvm` on Linux hosts). For BrowserStack mode (`EXECUTION_MODE=BrowserStack`), no emulator is needed in the container — pass your BrowserStack credentials as env vars instead.

## Allure reports

Test results are written to `allure-results/` during every run. The Docker container
automatically generates a single-file HTML report at `allure-report/index.html` after
tests finish. The file is fully self-contained and can be opened in any browser.

### Docker

Mount a host directory to receive the report:

```bash
docker run --rm \
  --device /dev/kvm \
  -v $(pwd)/allure-report:/src/allure-report \
  appium-tests
```

The report is at `./allure-report/index.html`.

Alternatively, copy it out after a named run:

```bash
docker run --name appium-run appium-tests
docker cp appium-run:/src/allure-report/index.html ./allure-report.html
docker rm appium-run
```

### Local

Install the Allure CLI once:

```bash
brew install allure                      # macOS
npm install -g allure-commandline        # cross-platform
```

After running tests:

```bash
# Single self-contained HTML file (matches Docker output)
allure generate --single-file allure-results -o allure-report
open allure-report/index.html

# Or live interactive server
allure serve allure-results
```

## Project structure

```
AppiumTests/
├── Tests/
│   ├── BaseTest.cs                  # [SetUp]/[TearDown] — creates/quits Appium driver
│   ├── AndroidBrowserTests.cs
│   └── IosBrowserTests.cs
├── Appium/
│   ├── AppiumDriverFactory.cs       # Creates local and BrowserStack drivers
│   ├── Pages/
│   │   └── BaseAppiumPage.cs        # Base page object for Appium
│   └── Config/
│       ├── AppiumSettings.cs
│       ├── AndroidEmulatorConfig.cs
│       ├── IosSimulatorConfig.cs
│       └── BrowserStackConfig.cs         ← new
├── Config/
│   ├── ConfigurationLoader.cs       # Loads appsettings + env vars
│   ├── TestEnvironmentOptions.cs
│   └── BrowserStackOptions.cs            ← new
├── appsettings.json
├── appsettings.Test.json            # Local overrides (not committed)
├── allureConfig.json
├── entrypoint.sh
└── Dockerfile
```
