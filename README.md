# Installation
## NixOS
Add following codeblock to your `configuration.nix`.
```nix
  environment.systemPackages = with pkgs; [
    steam-run
    # calc-game
    (import (builtins.fetchurl {
      url = "https://github.com/Laurids33/Calc/releases/download/release_v1.00.01/package.nix";
      sha256 = "0v4h30x2bhybhk2jng38nr6wld7wi3ki0c451r31jpdw87c6h0k1";
    }) {inherit pkgs;})
  ];
```
Run either the .desktop file or use `steam-run calc-game`.
