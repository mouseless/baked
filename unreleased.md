# Unreleased

## Features

- To support `Publicize.Fody` weaving, domain model now treats members with
  `EditorBrowsable(State=Advanced)` as private
  - `IsOriginallyPublic()` extension is introduced to check if attribute is
    present on a member info
- [Layers / Domain](../layers/domain.md#proxifying-entities) is updated to contain
  a guid to enable proxifying in domain assemblies
