;Mario can only pass in from above of this block
;by smkdan

JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteV : JMP SpriteH : JMP MarioCape : JMP MarioFireBall

MarioBelow:
	LDY #$10	;act like tile 130
	LDA #$30
	STA $1693

MarioAbove:
MarioSide: 
SpriteV: 
SpriteH: 
MarioCape:
MarioFireBall:
	RTL