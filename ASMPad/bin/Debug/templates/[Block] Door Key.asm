;generates a door when a sprite is carried into it, then makes the key dissappear
;by smkdan
;act like whatever
JMP Check : JMP Check : JMP Check : JMP Return : JMP Return : JMP Return : JMP Return
;don't touch those

;sprite to teleport make door appear when carried, set to key by default
!SPRITECARRY = $80
;sound to make when door appears
!APPEARSOUND = $00

;this needs to be set to the map16 of the top and bottom blocks
!MAP16TOP = $0480
!MAP16BOT = $0481

;DON'T TOUCH
!MAP16DOORTOP = $001F
!MAP16DOORBOT = $0020

Check:
	LDX #$0B	;12 sprites to loop through
Loop:
	LDA $14C8,x	;test sprite status
	CMP #$0B	;must be being carried
	BNE NextSprite
	LDA $9E,x	;test sprite #
	CMP #!SPRITECARRY

	STZ $14C8,x	;sprite vanishes
;make door appear
	PHY		;map16hi

	REP #$20	;16bitA
	LDA $03		;load block #
	CMP #!MAP16TOP	;top
	BEQ ChangeTop
	CMP #!MAP16BOT
	BEQ ChangeBot	;bttom

	LDX #$00	;you screwed up
	STX $2121	
	LDX #$1F
	STX $2122
	LDX #$00
	STX $2122
			
	STP

ChangeBot:
	LDA #!MAP16DOORBOT
	PHA

	LDA $98		;vanish block above this one
	AND #$FFF0	;16px
	PHA		;preserve
	SEC
	SBC #$0010	;16px
	STA $98
	
	LDA #!MAP16DOORTOP
	PHA

	BRA DoorAppear

ChangeTop:
	LDA #!MAP16DOORTOP
	PHA

	LDA $98		;vanish block below
	AND #$FFF0	;16px
	PHA
	CLC
	ADC #$0010
	STA $98

	LDA #!MAP16DOORBOT
	PHA

DoorAppear:
	PLA
	STA $03
	JSR ChangeBlock	;change block displaced
	JSR SUB_SMOKE	;make smoke

	PLA		;restore Y block
	STA $98

	PLA
	STA $03
	JSR ChangeBlock	;change block 
	JSR SUB_SMOKE	;make smoke

	PLY		;restore map16hi

	SEP #$20
	LDA #!APPEARSOUND
	STA $1DFC	;make sound

	RTL		;and return

NextSprite:
	DEX		;next sprite
	BPL Loop

Return:
	RTL

;borrwed stuff
;A smoke routine
;===============

SUB_SMOKE:
		    PHP
		    SEP #$20
           	    LDY #$03                ; \ find a free slot to display effect
FINDFREE:
	            LDA $17C0,y             ;  |
                    BEQ FOUNDONE            ;  |
                    DEY                     ;  |
                    BPL FINDFREE            ;  |
		    PLP
                    RTS                     ; / return if no slots open

FOUNDONE:
	            LDA #$01                ; \ set effect graphic to smoke graphic
                    STA $17C0,y             ; /
                    LDA $98               ; \ smoke y position = generator y position
		    AND #$F0
                    STA $17C4,y             ; /
                    LDA #$18                ; \ set time to show smoke
                    STA $17CC,y             ; /
                    LDA $9A               ; \ load generator x position and store it for later
		    AND #$F0
                    STA $17C8,y             ; /
                    LDX $15E9
		    PLP
                    RTS

;$03 contains block to change to
;16bit locations in $98 and $9A
ChangeBlock:
	PHP
        REP #$30                  ; Index (16 bit) Accum (16 bit)
        LDA $0003
        JSR ADDR_00814A
        PLP
        RTS
	
ADDR_008000:        PLX
                    PLB
                    PLP
                    RTS
ADDR_008004:
                    PHP
                    SEP #$20                  ; Accum (8 bit)
                    PHB
                    LDA $7EBD05
                    BNE ADDR_008012
                    LDA #$00
                    BRA ADDR_008014

ADDR_008012:        LDA #$30
ADDR_008014:        PHA
                    PLB
                    REP #$30                  ; Index (16 bit) Accum (16 bit)
                    PHX
                    LDA $9A
                    STA $0C
                    LDA $98
                    STA $0E
                    LDA #$0000
                    SEP #$20                  ; Accum (8 bit)
                    LDA $5B
                    STA $09
                    LDA $1933
                    BEQ ADDR_008031
                    LSR $09
ADDR_008031:        LDY $0E
                    LDA $09
                    AND #$01
                    BEQ ADDR_008047
                    LDA $9B
                    STA $00
                    LDA $99
                    STA $9B
                    LDA $00
                    STA $99
                    LDY $0C
ADDR_008047:        CPY #$0200
                    BCS ADDR_008000
                    LDA $1933
                    ASL
                    TAX
                    LDA $BEA8,X
                    STA $65
                    LDA $BEA9,X
                    STA $66
                    STZ $67
                    LDA $1925
                    ASL
                    TAY
                    LDA ($65),Y
                    STA $04
                    INY
                    LDA ($65),Y
                    STA $05
                    STZ $06
                    LDA $9B
                    STA $07
                    ASL
                    CLC
                    ADC $07
                    TAY
                    LDA ($04),Y
                    STA $6B
                    STA $6E
                    INY
                    LDA ($04),Y
                    STA $6C
                    STA $6F
                    LDA #$7E
                    STA $6D
                    INC A
                    STA $70
                    LDA $09
                    AND #$01
                    BEQ ADDR_008099
                    LDA $99
                    LSR
                    LDA $9B
                    AND #$01
                    BRA ADDR_00809E

ADDR_008099:        LDA $9B
                    LSR
                    LDA $99
ADDR_00809E:        ROL
                    ASL
                    ASL
                    ORA #$20
                    STA $04
                    CPX #$0000
                    BEQ ADDR_0080AF
                    CLC
                    ADC #$10
                    STA $04
ADDR_0080AF:        LDA $98
                    AND #$F0
                    CLC
                    ASL
                    ROL
                    STA $05
                    ROL
                    AND #$03
                    ORA $04
                    STA $06
                    LDA $9A
                    AND #$F0
                    LSR
                    LSR
                    LSR
                    STA $04
                    LDA $05
                    AND #$C0
                    ORA $04
                    STA $07
                    REP #$20                  ; Accum (16 bit)
                    LDA $09
                    AND #$0001
                    BNE ADDR_0080F2
                    LDA $1A
                    SEC
                    SBC #$0080
                    TAX
                    LDY $1C
                    LDA $1933
                    BEQ ADDR_008109
                    LDX $1E
                    LDA $20
                    SEC
                    SBC #$0080
                    TAY
                    BRA ADDR_008109

ADDR_0080F2:        LDX $1A
                    LDA $1C
                    SEC
                    SBC #$0080
                    TAY
                    LDA $1933
                    BEQ ADDR_008109
                    LDA $1E
                    SEC
                    SBC #$0080
                    TAX
                    LDY $20
ADDR_008109:        STX $08
                    STY $0A
                    LDA $98
                    AND #$01F0
                    STA $04
                    LDA $9A
                    LSR
                    LSR
                    LSR
                    LSR
                    AND #$000F
                    ORA $04
                    TAY
                    PLA
                    SEP #$20                  ; Accum (8 bit)
                    STA [$6B],Y
                    XBA
                    STA [$6E],Y
                    XBA
                    REP #$20                  ; Accum (16 bit)
                    ASL
                    TAY
                    PHK
        	    PER $0015
                    LDA $7EBD05
                    BNE ADDR_008140
                    PEA $804C
                    JMP $00C0FB

                    BRA ADDR_008147

ADDR_008140:        PEA $805F
                    JMP $30C0FF

ADDR_008147:        PLB
                    PLP
                    RTS
ADDR_00814A:	
                    PHP
                    REP #$30                  ; Index (16 bit) Accum (16 bit)
                    PHY
                    PHX
                    TAX
                    LDA $03
                    PHA
                    JSR ADDR_008004
                    PLA
                    STA $03
                    PLX
                    PLY
                    PLP
                    RTS
