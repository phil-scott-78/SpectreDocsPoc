---
title: "Capabilities Reference"
description: "Complete reference for terminal capabilities, environment variables, and CI detection in Spectre.Console"
uid: "console-capabilities-reference"
order: 7060
---

This reference documents the terminal capabilities that Spectre.Console detects, the environment variables that influence detection, and the CI systems that are automatically recognized.

<Screenshot src="/assets/capabilities.svg" alt="Demonstration of capabilities being detected by env variables" />

## Detected Capabilities

These capabilities are stored in `AnsiConsole.Profile.Capabilities`:

| Capability | What It Means |
|------------|---------------|
| **ColorSystem** | How many colors: none, 8, 16, 256, or 16 million (TrueColor) |
| **Ansi** | Can the terminal understand escape codes for styling? |
| **Links** | Can text be clickable hyperlinks? |
| **Interactive** | Is someone there to answer prompts? |
| **Unicode** | Can it display Unicode characters like `─` and `│`? |
| **Legacy** | Is this an old-style Windows console? |

## Environment Variables

These environment variables influence capability detection:

| Variable | What It Does |
|----------|--------------|
| `NO_COLOR` | Set to any value to disable all colors. Follows the [NO_COLOR standard](https://no-color.org/). |
| `COLORTERM` | Set to `truecolor` or `24bit` to enable 24-bit color on Unix systems. |
| `TERM` | Your terminal type (like `xterm-256color`). Usually set automatically. |
| `ConEmuANSI` | Set to `On` if using ConEmu on Windows and colors aren't working. |

Example usage:

```bash
NO_COLOR=1 ./myapp
```

## CI Environment Detection

Spectre.Console automatically detects these CI/CD systems and adjusts capabilities accordingly:

| CI System | Environment Variable | Capability Changes |
|-----------|---------------------|-------------------|
| GitHub Actions | `GITHUB_ACTIONS=true` | Ansi=true, Interactive=false, Links=false |
| Azure Pipelines | `TF_BUILD` (non-empty) | Interactive=false |
| GitLab CI | `CI_SERVER=yes` | Interactive=false |
| Jenkins | `JENKINS_URL` | Interactive=false |
| Travis CI | `TRAVIS` | Interactive=false |
| TeamCity | `TEAMCITY_VERSION` | Interactive=false |
| AppVeyor | `APPVEYOR` | Interactive=false |
| Bitbucket Pipelines | `BITBUCKET_COMMIT` | Interactive=false |
| CircleCI | `CIRCLECI` | Interactive=false |
| Bamboo | `bamboo_buildNumber` | Interactive=false |
| Bitrise | `BITRISE_BUILD_URL` | Interactive=false |
| GoCD | `GO_SERVER_URL` | Interactive=false |
| MyGet | `BuildRunner=MyGet` | Interactive=false |
| Continua CI | `ContinuaCI.Version` | Interactive=false |

GitHub Actions receives special treatment: Spectre.Console enables ANSI colors since the runners support them, even when standard detection might not recognize it.

## See Also

- <xref:console-rendering-model> - How capability detection influences rendering
- <xref:console-color-reference> - Available colors at each color system level
