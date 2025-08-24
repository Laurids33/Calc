# SpaceShooter
## Installation
### NixOS
Add following codeblock to your `configuration.nix`.
```nix
  environment.systemPackages = with pkgs; [
    # calc-game
    (import (builtins.fetchurl {
      url = "https://github.com/Laurids33/Calc/releases/download/release_v1.00.01/package.nix";
      sha256 = "193yrhcws798crp0cw8qrjnfng2r5m181hbrxf7jg5fvjz2di2b5";
    }) {
      inherit pkgs;
      inherit (pkgs) stdenv fetchurl lib unzip steam-run;
    })
  ];
```
Use either the .desktop file or run `calc-game`.
