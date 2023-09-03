db $42

JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteH : JMP SpriteV : JMP Fireball : JMP Cape : JMP MarioHead : JMP MarioCenter : JMP MarioCorner

MarioBelow:
MarioAbove:
MarioSide:
	JSL $00F606 ; Instant Kill..
SpriteH:
SpriteV:
Fireball:
Cape:
MarioHead:
MarioCenter:
MarioCorner:
	RTL 