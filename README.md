# ArtifactSdkDotNet
C# SDK for working with Valve's APIs for Artifact

Original documentation here: https://github.com/ValveSoftware/ArtifactDeckCode/

### Contents

* Core - shared model classes and utilities used across all components
* DeckCode - DeckCode encoder and decoder (equivalent of PHP examples from above link)
* CardsetApi - Simple wrapper to download latest card set information

### TODO

- [ ] Better interfaces for model classes to work with each other (Deck could have helper functions taking in Cards from the Cardset for example)
- [ ] Negative tests for DeckDecoder (should try to generate byte arrays that trigger all the possible exception cases)
- [ ] Intellisense Documentation
