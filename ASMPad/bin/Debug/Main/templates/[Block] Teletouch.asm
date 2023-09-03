db $42

JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteH : JMP SpriteV : JMP Fireball : JMP Cape : JMP MarioHead : JMP MarioCenter : JMP MarioCorner

MarioBelow:
MarioAbove:
MarioSide:
      LDA #$06             ;/ Teleport the player to screen destination.
      STA $71
      STZ $88
      STZ $89
SpriteH:
SpriteV:
Fireball:
Cape:
MarioHead:
MarioCenter:
MarioCorner:
	RTL 