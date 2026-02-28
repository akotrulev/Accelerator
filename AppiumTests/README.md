# AppiumTests (Appium mobile tests)

NUnit test project for mobile UI testing using [Appium](https://appium.io/). Includes an Appium driver factory, iOS/Android configuration helpers, and a base page object.

## Environment Setup

### 1. JDK 21

**macOS:**
```bash
brew install openjdk@21
sudo ln -sfn $(brew --prefix openjdk@21)/libexec/openjdk.jdk /Library/Java/JavaVirtualMachines/openjdk-21.jdk
```

Add to `~/.zshrc` (or `~/.bash_profile`):
```bash
export JAVA_HOME=$(/usr/libexec/java_home -v 21)
export PATH=$JAVA_HOME/bin:$PATH
```

Reload and verify:
```bash
source ~/.zshrc
java -version   # must show openjdk 21
```

---

### 2. Android SDK (without Android Studio)

Download command-line tools only from https://developer.android.com/studio#command-tools.

**macOS:**
```bash
mkdir -p ~/android-sdk/cmdline-tools
unzip commandlinetools-mac-*.zip -d ~/android-sdk/cmdline-tools
mv ~/android-sdk/cmdline-tools/cmdline-tools ~/android-sdk/cmdline-tools/latest
```

Add to `~/.zshrc` (or `~/.bash_profile`):
```bash
export ANDROID_HOME=~/android-sdk
export ANDROID_SDK_ROOT=$ANDROID_HOME
export PATH=$ANDROID_HOME/cmdline-tools/latest/bin:$ANDROID_HOME/platform-tools:$ANDROID_HOME/emulator:$PATH
```

Reload: `source ~/.zshrc`

**Windows:**
- Create SDK root: `mkdir %USERPROFILE%\android-sdk\cmdline-tools`
- Extract the downloaded zip so the inner `cmdline-tools` folder ends up at:
  `%USERPROFILE%\android-sdk\cmdline-tools\latest`
- Add to System Environment Variables (System Properties → Environment Variables):
  - `ANDROID_HOME` = `%USERPROFILE%\android-sdk`
  - `ANDROID_SDK_ROOT` = `%USERPROFILE%\android-sdk`
  - Append to `Path`:
    - `%ANDROID_HOME%\cmdline-tools\latest\bin`
    - `%ANDROID_HOME%\platform-tools`
    - `%ANDROID_HOME%\emulator`
- Open a new Command Prompt or PowerShell for the variables to take effect.

**Both platforms — install SDK components and create AVD:**
```bash
yes | sdkmanager --licenses
sdkmanager "platform-tools" "platforms;android-34" "build-tools;34.0.0" \
           "system-images;android-34;google_apis;x86_64" "emulator"
```
On Windows, replace `yes |` with answering `y` to each prompt (or pipe with `echo y |` in CMD).

Create AVD matching the project config (`Avd = "Pixel_6_API_34"`):
```bash
avdmanager create avd \
  -n Pixel_6_API_34 \
  -k "system-images;android-34;google_apis;x86_64" \
  -d "pixel_6"
```

Start the emulator:

**macOS / Linux:**
```bash
emulator -avd Pixel_6_API_34 -no-snapshot &
adb wait-for-device
adb devices   # should list the emulator
```

**Windows (Command Prompt):**
```cmd
start emulator -avd Pixel_6_API_34 -no-snapshot
adb wait-for-device
adb devices
```

**Windows (PowerShell):**
```powershell
Start-Process emulator -ArgumentList "-avd Pixel_6_API_34 -no-snapshot"
adb wait-for-device
adb devices
```

---

### 3. Node.js and npm

```bash
brew install node
```

Verify:
```bash
node -v   # e.g. v20.x.x
npm -v    # e.g. 10.x.x
```

---

### 4. Appium

```bash
npm install -g appium
appium driver install uiautomator2   # Android
appium driver install xcuitest       # iOS (macOS only)
```

Verify full setup:
```bash
appium driver list --installed
appium-doctor --android   # requires: npm install -g appium-doctor
```

Start the server (only needed if not using `AppiumLocalService` — BaseTest auto-starts it):
```bash
appium --address 127.0.0.1 --port 4723
```

---

### 5. .NET project

```bash
dotnet restore AppiumTests/AppiumTests.csproj
dotnet build AppiumTests/AppiumTests.csproj
```

Configure the target device in `appsettings.json` or via environment variables.

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
