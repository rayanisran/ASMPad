db $42

JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteV : JMP SpriteH : JMP MarioCape : JMP MarioFireBall : JMP MarioCorner : JMP MarioHeadinside : JMP MarioBodyinside
MarioCape:
MarioFireBall:
	RTL
MarioAbove:
	LDA #$08
	STA $1DFC
	LDA $15
	AND #$80
	BNE held
	LDA #$AF
	STA $7D
	RTL
held:
	LDA #$9F
	STA $7D
MarioBelow:
MarioHeadinside:
MarioSide:
MarioCorner:
MarioBodyinside:
	RTL
SpriteV:
	LDA #$08
	STA $1DFC
	LDA #$8F
	STA $AA,x
SpriteH:
Return:
	RTL
