# cuidgen

[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/visus-io/cuidgen/ci.yaml?style=for-the-badge&logo=github)](https://github.com/visus-io/cuidgen/actions/workflows/ci.yaml)
[![Code Quality](https://img.shields.io/codacy/grade/2312e89be42844938b9ef20bbc20490d?style=for-the-badge&logo=codacy)](https://app.codacy.com/gh/visus-io/cuidgen/dashboard)

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

### Example Usage

cuidgen is designed to be as simple as possible and is perfect for usage in scripting.

```shell
# generate a CUIDv2 value with a length of 10
$ cuidgen -l:10
fd59iobs0p
```

> :memo: cuidgen by default will generate CUIDv2 values

### Command Arguments

| Argument       | Description                                               | Default Value | Accepted Values |
|----------------|-----------------------------------------------------------|---------------|-----------------|
| -l:\<length\>  | Desired length of the CUID value (only applies to CUIDv2) | 24            | 4-32            |
| -g:\<version\> | Generation of the CUID to generate                        | 2             | 1 or 2          |

