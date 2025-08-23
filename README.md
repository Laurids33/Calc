# Installation
## NixOS
Add following code block to your configuration.
```nix
  environment.systemPackages = with pkgs; [
    # calc-game
    (import (builtins.fetchurl {
      url = "https://github.com/Laurids33/Calc/releases/download/release_v1.00.01/package.nix";
      sha256 = "0v4h30x2bhybhk2jng38nr6wld7wi3ki0c451r31jpdw87c6h0k1";
    }) {inherit pkgs;})
  ];
```
