# cuidgen

[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/visus-io/cuidgen/ci.yml?style=for-the-badge&logo=github)](https://github.com/visus-io/cuidgen/actions/workflows/ci.yaml)
[![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/visus%3Acuidgen?server=https%3A%2F%2Fsonarcloud.io&style=for-the-badge&logo=sonarcloud&logoColor=white)](https://sonarcloud.io/summary/overall?id=visus%3Acuidgen)
[![Sonar Coverage](https://img.shields.io/sonar/coverage/visus%3Acuidgen?server=https%3A%2F%2Fsonarcloud.io&style=for-the-badge&logo=sonarcloud&logoColor=white)](https://sonarcloud.io/summary/overall?id=visus%3Acuidgen)

[![Nuget](https://img.shields.io/nuget/v/cuidgen.tool?style=for-the-badge&logo=nuget&label=stable)](https://www.nuget.org/packages/cuidgen.tool)
![Downloads](https://img.shields.io/nuget/dt/cuidgen.tool?style=for-the-badge&logo=nuget)
![GitHub](https://img.shields.io/github/license/visus-io/cuidgen?style=for-the-badge)

A command-line utility based on [cuid.net](https://github.com/visus-io/cuid.net/) for generating collision-resistant ids.
You can read more about CUIDs from the [official project website](https://github.com/paralleldrive/cuid2).

---

## Installation

### [.NET Tool](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools)

```shell
dotnet tool install --global cuidgen.tool
```

If you prefer, cuidgen is also available as a portable binary for Windows, Mac and Linux for download from
the [Releases](https://github.com/visus-io/cuidgen/releases) page.

## Usage

cuidgen is designed to be as simple as possible and is perfect for usage in scripting.

> [!NOTE]
> cuidgen by default generates CUIDv2 values with a length of 24 characters.

### Examples

```shell
# Generate a default CUIDv2 (length 24)
$ cuidgen
ukzsdmvhjyhwb9mkguvwegbv

# Generate a CUIDv2 with a custom length
$ cuidgen -l 10
qrlkdu9vri

# Generate a CUIDv1
$ cuidgen -g 1
cmjmvcts200016437xe4i0jfu

# Generate multiple CUIDs at once
$ cuidgen -n 5
z1dimahkx6si6mzfumzz4lpr
vy0467zugnmo87kjdpao4r8d
qh410ftu8mb21iw8aqp5l600
czlgawafm116s2n4eyaqp8r3
g13maok93mbgdqruv7mr3djl
```

### Command Arguments

| Argument            | Description                                               | Default | Valid Range  |
|---------------------|-----------------------------------------------------------|---------|--------------|
| `-g, --generation`  | Generation of CUID to generate (1 or 2)                   | 2       | 1-2          |
| `-l, --length`      | Desired length of the CUID (only applies to CUIDv2)       | 24      | 4-32         |
| `-n, --number`      | Number of CUIDs to generate                               | 1       | 1-1000       |
| `-v, --version`     | Display version information                               | -       | -            |
| `-h, --help`        | Display help information                                  | -       | -            |

