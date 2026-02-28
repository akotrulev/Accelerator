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
    "BaseUrl": "",
    "ApiUri": "",
    "Username": "",
    "Password": ""
  }
}
```

Appium-specific settings (app path, device name, platform version, etc.) are passed directly to `AppiumDriverFactory` when creating a driver instance.

## Running tests locally

From the repository root:

```bash
dotnet test AppiumTests/AppiumTests.csproj
```

Options:

```bash
# Verbose output
dotnet test AppiumTests --logger "console;verbosity=detailed"

# Run a single test class
dotnet test AppiumTests --filter "FullyQualifiedName~BaseTest"
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

> **Note:** Running Appium tests inside Docker requires a connected device or emulator accessible from within the container. Typically you would pass `APPIUM_HOST` and device-specific environment variables at `docker run` time.

## Allure reports

After a test run, results are written to `allure-results/`. Generate and open the report with:

```bash
allure serve allure-results
```

## Project structure

```
AppiumTests/
├── Program.cs                       # Entry point
├── Tests/
│   └── BaseTest.cs                  # [SetUp]/[TearDown] — creates/quits Appium driver
├── Appium/
│   ├── AppiumDriverFactory.cs       # Creates iOS and Android drivers
│   ├── Pages/
│   │   └── BaseAppiumPage.cs        # Base page object for Appium
│   └── Config/
│       ├── AppiumSettings.cs
│       ├── IosSimulatorConfig.cs
│       └── AndroidEmulatorConfig.cs
├── Config/
│   ├── ConfigurationLoader.cs       # Loads appsettings + env vars
│   └── TestEnvironmentOptions.cs
├── appsettings.json
├── appsettings.Test.json            # Local overrides (not committed)
├── allureConfig.json
└── Dockerfile
```
