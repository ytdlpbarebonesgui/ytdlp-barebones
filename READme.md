
# YT-DLP Barebones
---

A yt-dlp assistant app made for Windows yt-dlp users who prefer the original way of downloading videos using the command prompt or batch files. The goal is to keep the original CMD experience while doing only the "bare minimum" or, should I say... barebones. 💀🦴

#### Removing the tedious tasks of...
- Manually pasting your preferred options for every download.
- Manually installing or updating the yt-dlp executable.
- Navigating through folders to find your downloaded audio and videos.
- Creating batch files for multiple URLs or repeated configurations.

### Important Notes
- **This app isn't beginner-friendly.** It is built for Windows users who already have basic knowledge and experience downloading with yt-dlp using the command prompt.
- **This app is not a replacement for yt-dlp.** It provides a graphical interface for preparing and executing yt-dlp commands.
- **Modded versions and unofficial forks of yt-dlp are not supported.**
- **yt-dlp and FFmpeg are not bundled with this application.** The app can download or update the official yt-dlp executable, but FFmpeg must be installed separately.
- Downloading may still require additional configuration, browser cookies, authentication, or other yt-dlp options depending on the website.

## Features

### General

#### 1. YT-DLP Command and Configuration

**Typing in your yt-dlp command**
- Enter your preferred yt-dlp options in the command textbox.
- The command is retained between downloads until the application is closed.
- Do not include `yt-dlp` at the beginning of the textbox.
- Do not paste video URLs into the command textbox. Use the URL textbox instead.

**Loading Configuration**
- Load saved configurations using the comboboxes below the command textbox.
- The left combobox filters configurations by category.
- The right combobox lists the available configurations within the selected category.
- The application comes with predefined configurations. You can modify them or save your own.

**Saving Configuration**
- Enter your desired yt-dlp options, then click the Save Configuration button (💾).
- In the Save New Configuration window, you can:
  - Enter a name for your configuration.
  - Assign it to a configuration category to keep configurations organized.
- Saved configurations can be loaded, updated, downloaded, or deleted through the Configuration Manager.

**Configuration Manager**
- Click the Configuration Manager button (💼) to manage saved configurations.

*Select a config*
- The top-right combobox lists saved configurations.
- The category combobox lets you filter configurations by category.
- Select a configuration to view its saved command and manage it.

*Change config to...*
- Change the selected configuration's category to organize your saved configurations.

*Download the config (📥)*
- Download the selected configuration's command instead of manually copying its options into the command textbox.

*Update the config (💾)*
- Save changes to the selected configuration using the current command and configuration details.

*Delete the config (❌)*
- Delete the selected saved configuration after confirmation.

#### 2. Location/Directory Path Selector

Manage your preferred download directories without manually typing their paths.

**Adding a path (➕)**
- Click Add Path (➕) to open the Select Folder dialog.
- Select any folder you want to use as a download location.

**Selecting a path (🎯)**
- Select a directory from the list box to use it as the download location.

**Opening the path (📁)**
- Click Open Selected Path (📁) to open the selected directory in Windows File Explorer.

**Moving paths up or down (⬆️⬇️)**
- Use Move Up (⬆️) or Move Down (⬇️) to rearrange the directory list.

**Removing a path (❌)**
- Click Remove Selected Path (❌) and confirm the action when prompted.
- Removing a path only removes it from the application's list. The actual folder and its contents will remain untouched.

#### 3. URL(s) to Download

- Paste one or more video URLs into the URL textbox.
- Pasting a URL automatically creates a new line for the next URL.
- Duplicate URLs are not allowed.
- The application passes the URLs to yt-dlp when a download is started.

### Optional Features

#### 1. Files (Cookies, Authentication, etc.)

- Add file paths for files required by your yt-dlp command, such as cookie files or other input files.
- Use the file placeholders provided by the application to reference selected files in your command.
- This is useful for commands that require authentication or additional input files.
- The application does not bypass website restrictions or provide authentication credentials for you.

#### 2. Download Sections

- Specify sections of a video to download using the application's download-section controls.
- Use this feature when you only need a particular part of a video rather than the entire file.
- The generated options are passed to yt-dlp, so supported formats and cutting behavior depend on yt-dlp and the installed FFmpeg components.

#### 3. Exec (Shell Command)

- Execute additional shell commands through the application's external command prompt workflow.
- Intended for users who already understand command-line syntax and the commands they are executing.
- Use caution when entering shell commands, as they can modify files, execute programs, or affect your system.

## Settings

The separate Settings window provides application preferences, including:

- **Command Prompt Mode** — Configure the application's command prompt behavior.
- **Auto Check Updates** — Enable or disable automatic checks for yt-dlp updates.
- **yt-dlp Version** — View the detected current version of yt-dlp.

The application uses the official yt-dlp update mechanism (`yt-dlp -U`) rather than maintaining a separate update system for the executable.

## Requirements

- **Operating System:** Windows.
- **yt-dlp:** The official yt-dlp executable. The application can download or update it.
- **FFmpeg:** Required for operations that depend on FFmpeg, including many audio conversions, merging formats, and download-section processing. Install it separately when needed.
- **Internet connection:** Required for downloading online media, obtaining yt-dlp updates, and checking for updates.

Some yt-dlp features may require additional dependencies or configuration.

## Usage

1. Launch YT-DLP Barebones.
2. Install or update yt-dlp if necessary.
3. Select or add your preferred download directory.
4. Enter your yt-dlp options or load a saved configuration.
5. Add the URL or URLs you want to download.
6. Configure any optional files or download sections required by your command.
7. Start the download and monitor the external command prompt.
8. Find your downloaded files in the selected output directory.

For yt-dlp command syntax, supported options, troubleshooting, and website-specific requirements, refer to the official documentation:

- [yt-dlp GitHub Repository](https://github.com/yt-dlp/yt-dlp)
- [yt-dlp README and Options](https://github.com/yt-dlp/yt-dlp#usage-and-options)
- [yt-dlp FAQ](https://github.com/yt-dlp/yt-dlp/wiki/FAQ)

## Limitations

- Windows only.
- Requires familiarity with yt-dlp commands and command-line usage.
- Does not bundle FFmpeg or other external media-processing tools.
- Does not support modded yt-dlp forks.
- Does not guarantee that every website, URL, or yt-dlp option will work.
- Download availability and behavior depend on yt-dlp, the target website, and any required authentication or external dependencies.

## Licenses & Third-Party Notices

This project is licensed under the MIT License. Copyright (c) 2025 SyberGen.

See the [LICENSE](LICENSE) file for details.

### Third-Party Components

- **Newtonsoft.Json** — Licensed under the MIT License. Copyright (c) James Newton-King.
- **yt-dlp** — Licensed under the Unlicense. Source: https://github.com/yt-dlp/yt-dlp
  - This application does not bundle yt-dlp. It can download the official `yt-dlp.exe` at runtime and use yt-dlp's own update mechanism.
- **FFmpeg** — Components are available under LGPL and GPL licenses, depending on the build and included components. This application does not bundle FFmpeg; users must install it separately.

Third-party components retain their respective licenses and copyrights. Refer to their official repositories and distributions for complete license terms.