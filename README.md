# cuidgen


[![Continuous Integration](https://github.com/visus-io/cuidgen/actions/workflows/ci.yaml/badge.svg)](https://github.com/visus-io/cuidgen/actions/workflows/ci.yaml)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/2312e89be42844938b9ef20bbc20490d)](https://app.codacy.com/gh/visus-io/cuidgen/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

[![Nuget](https://img.shields.io/nuget/vpre/cuidgen.tool)](https://www.nuget.org/packages/cuidgen.tool)
![Nuget](https://img.shields.io/nuget/dt/cuidgen.tool)
![GitHub](https://img.shields.io/github/license/visus-io/cuidgen?logo=github&style=flat)

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

