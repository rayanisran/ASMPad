;a regular coin
;by Iceguy
;to be used with BTSD

;NOTE: YOU CAN REPLACE TILE 2B WITH THIS BLOCK, AND MAKE IT ACT LIKE TILE 25!


!COIN_TO_GIVE = $01		;# of coins to give (1)
				;DO NOT ADD A STUPID VALUE LIKE 1321 OTHERWISE IT WILL NOT
				;WORK!

!SOUND = $01			;sound to play for a coin
!PORT = $1DFC			;which port to play from
!1_UP_SFX = $05			;sound to play for the 1up (100 coins)
!1_UP_PORT = $1DFC		;1up port
!1_UP_COINS = $64			;coins needed for a 1-up, and resetting the counter
				;it's in hex (64 = 100 in dec)
!1_UPS = $01			;1ups to give when collecting 100 coins

JMP Main : JMP Main : JMP Main : JMP return : JMP return : JMP return : JMP return

MakeSolid:
	LDY #$01
	LDA #$30
	STA $1693
	RTL

Main:
	LDA $14AD
	BNE MakeSolid
	LDA #!SOUND
	STA !PORT	;play the sfx of a regular coin
	LDA $0DBF
	CLC
	ADC #!COIN_TO_GIVE	;add coins
	STA $0DBF
	DEC $0DC0
;this is so that a glitter effect is played when collecting
;a coin
	LDA $7F
	ORA $81
	BNE Return
	LDY.B #$03
LoopStart:
	LDA.W $17C0,Y
	BEQ CreateGlitter
	DEY
	BPL LoopStart
CreateGlitter:
	LDA #$05
	STA $17C0,Y
	LDA $9A
	AND #$F0
	STA $17C8,Y
	LDA $98
	AND #$F0
	STA $17C4,Y
	LDA $1933
	BEQ ADDR_00FD97
	LDA $9A
	SEC
	SBC $26
	AND #$F0
	STA $17C8,Y
	LDA $98
	SEC
	SBC $28
	AND #$F0
	STA $17C4,Y
ADDR_00FD97:
	LDA #$10
	STA $17CC,Y
	LDA $0DBF
			;this is so that the counter does not display hex
	CMP #!1_UP_COINS	;and go to values above 99 (like A0, B5, I4 etc.)
	BEQ STZ_COIN
	BCS STZ_COIN
	BNE ERASE_COIN
	
STZ_COIN:
	LDA $0DBE
	CLC
	ADC #!1_UPS 	;add 1-ups
	STA $0DBE
	LDA #!1_UP_SFX
	STA !1_UP_PORT
	STZ $0DBF	;completely erase coin counter
	PHY		;preserve map16 high
	LDA #$02	;erase self
	STA $9C
	JSL $00BEB0	;generate blank block
	PLY		;restore map16 high
	RTL	
ERASE_COIN:
	PHY		;preserve map16 high
	LDA #$02	;erase self
	STA $9C
	JSL $00BEB0	;generate blank block
	PLY		;restore map16 high
	RTL
return:
	RTL