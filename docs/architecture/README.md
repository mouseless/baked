---
position: 3
---

# Architecture

> TBD

Layers, features and implementations

- layer exposes corresponding tech and configurators
  - layer is named after its domain/protocol e.g. Do.Database, Do.Messaging,
    Do.Rest, Do.Grpc, Do.Monitoring, Do.IoC
  - not opinionated, but still provides configuration helpers and stuff
  - might introduce its own configuration classes
  - might introduce its own worker classes as well
  - introduces one internal system component like RDBMS, DocumentDB, MQ, HTTP
    Server etc.
    - do not create a layer for external system components like AWS S3
- feature.abstraction exposes interfaces, primitives and default classes
  - feature.abstraction is named with the feature name only e.g. Do.Auth,
    Do.Sql, Do.Nosql, Do.Fs, Do.Log, Do.PubSub etc.
- feature.implementation depends on layer(s) and other feature.abstraction(s)
  as well as its own feature.abstraction
  - a feature implementation would require a layer, or a feature strictly
  - or it might optionally use a layer or a feature and works well without them
    as well
  - a feature implementation is named after its design or technology e.g.
    Do.Auth.Auth0, Do.Fs.Aws, Do.Sql.EfCore?, Do.PubSub.RabbitMq?
  - might introduce an external system component like auth0, keycloak, aws s3
    etc.
