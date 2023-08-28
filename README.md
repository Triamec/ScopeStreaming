# Scope Streaming

[![TAM - API](https://img.shields.io/static/v1?label=TAM&message=API&color=b51839)](https://www.triamec.com/en/tam-api.html)

Process `.TAMpbf` files produced with the TAM System Explorer scope according to [application note AN132](https://www.triamec.com/en/documents.html).

This sample can be taken as a template to start with. Without modification, the output just looks like this:

```
Processed 100000 samples.
Processed 100000 samples.
Processed 100000 samples.
Processed 100000 samples.
Processed 100000 samples.
Processed 100000 samples.
Processed 100000 samples.
Processed 100000 samples.
Processed 59995 samples.
```

If `exportToCsv` is set to `true`, the data is exported as `CSV` in a format which can be read by the TAM System Explorer.
