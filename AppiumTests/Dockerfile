FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- Java 21 (required by Appium UiAutomator2 driver) ---
RUN apt-get update && apt-get install -y openjdk-21-jdk curl unzip && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

ENV JAVA_HOME=/usr/lib/jvm/java-21-openjdk-amd64
ENV PATH=$JAVA_HOME/bin:$PATH

# --- Android SDK command-line tools ---
ENV ANDROID_HOME=/opt/android-sdk
ENV ANDROID_SDK_ROOT=$ANDROID_HOME
ENV PATH=$ANDROID_HOME/cmdline-tools/latest/bin:$ANDROID_HOME/platform-tools:$PATH

RUN mkdir -p $ANDROID_HOME/cmdline-tools && \
    curl -o /tmp/cmdline-tools.zip \
      https://dl.google.com/android/repository/commandlinetools-linux-11076708_latest.zip && \
    unzip /tmp/cmdline-tools.zip -d /tmp/android && \
    mv /tmp/android/cmdline-tools $ANDROID_HOME/cmdline-tools/latest && \
    rm -rf /tmp/cmdline-tools.zip /tmp/android

RUN yes | sdkmanager --licenses && \
    sdkmanager "platform-tools" "platforms;android-34" "build-tools;34.0.0"

# --- Node.js and Appium (uiautomator2 for Android, xcuitest for iOS) ---
ENV APPIUM_HOME=/usr/local/lib/appium
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g appium && \
    appium driver install uiautomator2 && \
    appium driver install xcuitest && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# --- .NET project ---
COPY ["AppiumTests/AppiumTests.csproj", "AppiumTests/"]
RUN dotnet restore "AppiumTests/AppiumTests.csproj"

COPY . .

RUN dotnet build "AppiumTests/AppiumTests.csproj" -c Release --no-restore

CMD ["dotnet", "test", "AppiumTests/AppiumTests.csproj", "--no-build", "-c", "Release"]
