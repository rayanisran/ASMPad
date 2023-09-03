db $42

JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteH : JMP SpriteV : JMP Fireball : JMP Cape : JMP MarioHead : JMP MarioCenter : JMP MarioCorner

MarioBelow:
MarioAbove:
MarioSide:
	JSL $00F5B7 ; Hurt Mario.
SpriteH:
SpriteV:
Fireball:
Cape:
MarioHead:
MarioCenter:
MarioCorner:
	RTL 