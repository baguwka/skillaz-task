# Usage

### Main

0. Download (https://github.com/baguwka/skillaz-task/releases/tag/1.0) or build.
1. Run `LinkShortener.Api.exe`
2. go to `localhost:5000/swagger`

# Requirements
- MongoDB service.
- .Net Core 2.1 (lts)

# Notes
In order to achive non-sequiential ids generation you need to resolve issue #2. Current implementation allows to iterate ids by alphabetical order.
