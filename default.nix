{ pkgs ? import <nixpkgs> { config.allowUnfree = true; } }:

let
  version = "1.00.01";
  src = pkgs.fetchzip {
    url = "https://github.com/Laurids33/Calc/releases/download/release_v1.00.01/Calc-1.00.01-Linux.zip";
    sha256 = "sha256-LypawG9QuzyJ06oAoq56PJ9nAf9sUmPq2yMTYmDANZ0=";
  };
in
pkgs.stdenv.mkDerivation {
  pname = "calc";
  inherit version src;

  dontBuild = true;

  installPhase = ''
    mkdir -p $out/opt/calc
    cp -r * $out/opt/calc/

    # Startskript mit absolutem Pfad
    mkdir -p $out/bin
    cat > $out/bin/calc <<EOF
#!/bin/sh
exec ${pkgs.steam-run}/bin/steam-run $out/opt/calc/Calc.x86_64 "\$@"
EOF
    chmod +x $out/bin/calc

    # Icon installieren
    install -Dm644 Calc_Data/Resources/UnityPlayer.png \
      $out/share/icons/hicolor/256x256/apps/calc.png

    # Desktop-Datei mit absoluten Pfaden erstellen
    mkdir -p $out/share/applications
    cat > $out/share/applications/calc-game.desktop <<EOF
[Desktop Entry]
Name=Calc
Exec=$out/bin/calc
Icon=$out/share/icons/hicolor/256x256/apps/calc.png
Type=Application
Categories=Game;
EOF
  '';

  meta = with pkgs.lib; {
    description = "Unity-basiertes Calc-Spiel";
    homepage = "https://github.com/Laurids33/Calc";
    license = licenses.unfree;
    platforms = platforms.linux;
  };
}
