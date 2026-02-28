FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN apt-get update && apt-get install -y \
        openjdk-21-jdk curl unzip \
        libpulse0 libnss3 libgles2 libvulkan1 qemu-kvm \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

ENV JAVA_HOME=/usr/lib/jvm/java-21-openjdk-amd64
ENV PATH=$JAVA_HOME/bin:$PATH

ENV ANDROID_HOME=/opt/android-sdk
ENV ANDROID_SDK_ROOT=$ANDROID_HOME
ENV PATH=$ANDROID_HOME/cmdline-tools/latest/bin:$ANDROID_HOME/platform-tools:$ANDROID_HOME/emulator:$PATH
ENV LD_LIBRARY_PATH=$ANDROID_HOME/emulator/lib64:$ANDROID_HOME/emulator/lib64/qt/lib

RUN mkdir -p $ANDROID_HOME/cmdline-tools && \
    curl -o /tmp/cmdline-tools.zip \
      https://dl.google.com/android/repository/commandlinetools-linux-11076708_latest.zip && \
    unzip /tmp/cmdline-tools.zip -d /tmp/android && \
    mv /tmp/android/cmdline-tools $ANDROID_HOME/cmdline-tools/latest && \
    rm -rf /tmp/cmdline-tools.zip /tmp/android

RUN yes | sdkmanager --licenses && \
    sdkmanager \
      "platform-tools" "platforms;android-34" "build-tools;34.0.0" \
      "emulator" "system-images;android-34;google_apis;x86_64"

# Create AVD at build time (safe — only writes files, doesn't boot)
RUN echo "no" | avdmanager create avd \
      --name "Pixel_6_API_34" \
      --package "system-images;android-34;google_apis;x86_64" \
      --device "pixel_6" \
      --force

ENV APPIUM_HOME=/usr/local/lib/appium
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g appium allure-commandline && \
    appium driver install uiautomator2 && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

COPY ["AppiumTests/AppiumTests.csproj", "AppiumTests/"]
RUN dotnet restore "AppiumTests/AppiumTests.csproj"
COPY . .
RUN dotnet build "AppiumTests/AppiumTests.csproj" -c Release --no-restore

# Copy entrypoint last so script changes don't bust SDK/build cache
COPY AppiumTests/entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

ENTRYPOINT ["/entrypoint.sh"]
