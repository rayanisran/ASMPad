JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteV : JMP SpriteH : JMP MarioCape : JMP MarioFireBall

MarioAbove:
	LDY #$10	;act like tile 130
	LDA #$30
	STA $1693
MarioBelow:
MarioSide:
SpriteV:
SpriteH:
MarioCape:
MarioFireBall:
	RTL