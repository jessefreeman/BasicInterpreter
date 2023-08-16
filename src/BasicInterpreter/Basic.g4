grammar Basic;
options { caseInsensitive = true; }

//prog
//   : line + EOF
//   ;
prog
   : (lineWithoutNumber? line*) EOF
   | EOF
   ;
// line
//   : linenumber (amprstmt (COLON amprstmt?)* | COMMENT | REM)
//   ;
line
   : linenumber NEWLINE? (amprstmt (COLON amprstmt?)* | COMMENT | REM) NEWLINE?
   ;
lineWithoutNumber
  : NEWLINE? (amprstmt (COLON amprstmt?)* | COMMENT | REM) NEWLINE?
  ;
amperoper
   : AMPERSAND
   ;
//linenumber
//   : NUMBER
//   ;
linenumber
   : (WS | NEWLINE)? NUMBER
   ;

amprstmt
   : amperoper? statement (COMMA statement)*
   | COMMENT
   | REM
   ;
statement
   : CLS | LOAD | SAVE | TRACE | NOTRACE | FLASH | INVERSE | GR | NORMAL | SHLOAD | CLEAR | RUN | STOP | TEXT | HOME | HGR | HGR2
   | endstmt
   | returnstmt
   | restorestmt
   | amptstmt
   | popstmt
   | liststmt
   | storestmt
   | getstmt
   | recallstmt
   | nextstmt
   | instmt
   | prstmt
   | onerrstmt
   | hlinstmt
   | vlinstmt
   | colorstmt
   | speedstmt
   | scalestmt
   | rotstmt
   | hcolorstmt
   | himemstmt
   | lomemstmt
   | printstmt1
   | pokestmt
   | plotstmt
   | ongotostmt
   | ongosubstmt
   | ifstmt
   | forstmt1
   | forstmt2
   | inputstmt
   | tabstmt
   | dimstmt
   | gotostmt
   | gosubstmt
   | callstmt
   | readstmt
   | hplotstmt
   | vplotstmt
   | vtabstmnt
   | htabstmnt
   | waitstmt
   | datastmt
   | xdrawstmt
   | drawstmt
   | defstmt
   | letstmt
   | includestmt
   ;
vardecl
   : var_ (LPAREN exprlist RPAREN)*
   ;
// printstmt1
//   : PRINT expression?
//   ;
//printstmt1
//   : PRINT exprlist?
//   ;
printstmt1
   : PRINT exprWithSeparator*
   ;
exprWithSeparator
   : expression separator?
   ;
separator
   : SEMICOLON
   | COMMA
   ;
getstmt
   : GET exprlist
   ;
letstmt
   : LET? variableassignment
   ;
// variableassignment
//   : vardecl EQ exprlist
//   ;
variableassignment
   : stringVarDecl EQ exprlist // For string variables
   | vardecl EQ exprlist      // For numeric variables
   ;
stringVarDecl
   : var_ DOLLAR
   ;
relop
   : GTE
   | GT EQ
   | EQ GT
   | LTE
   | LT EQ
   | EQ LT
   | neq
   | EQ
   | GT
   | LT
   ;
neq
   : LT GT
   ;
ifstmt
   : IF expression THEN? (statement | linenumber)
   ;
forstmt1
   : FOR vardecl EQ expression TO expression (STEP expression)? (statement NEXT vardecl?)?
   ;
forstmt2
   : FOR vardecl EQ expression TO expression (STEP expression)?
   ;
nextstmt
   : NEXT (vardecl (',' vardecl)*)?
   ;
inputstmt
   : INPUT (STRINGLITERAL (COMMA | SEMICOLON))? varlist
   ;
readstmt
   : READ varlist
   ;
dimstmt
   : DIM varlist
   ;
gotostmt
   : GOTO linenumber
   ;
gosubstmt
   : GOSUB expression
   ;
pokestmt
   : POKE expression COMMA expression
   ;
callstmt
   : CALL exprlist
   ;
hplotstmt
   : HPLOT (expression COMMA expression)? (TO expression COMMA expression)*
   ;
vplotstmt
   : VPLOT (expression COMMA expression)? (TO expression COMMA expression)*
   ;
plotstmt
   : PLOT expression COMMA expression
   ;
ongotostmt
   : ON expression GOTO linenumber (COMMA linenumber)*
   ;
ongosubstmt
   : ON expression GOSUB linenumber (COMMA linenumber)*
   ;
vtabstmnt
   : VTAB expression
   ;
htabstmnt
   : HTAB expression
   ;
himemstmt
   : HIMEM COLON expression
   ;
lomemstmt
   : LOMEM COLON expression
   ;
datastmt
   : DATA datum (COMMA datum?)*
   ;
datum
   : number
   | STRINGLITERAL
   ;
waitstmt
   : WAIT expression COMMA expression (COMMA expression)?
   ;
xdrawstmt
   : XDRAW expression (AT expression COMMA expression)?
   ;
drawstmt
   : DRAW expression (AT expression COMMA expression)?
   ;
defstmt
   : DEF FN? var_ LPAREN var_ RPAREN EQ expression
   ;
tabstmt
   : TAB LPAREN expression RPAREN
   ;
speedstmt
   : SPEED EQ expression
   ;
rotstmt
   : ROT EQ expression
   ;
scalestmt
   : SCALE EQ expression
   ;
colorstmt
   : COLOR EQ expression
   ;
hcolorstmt
   : HCOLOR EQ expression
   ;
hlinstmt
   : HLIN expression COMMA expression AT expression
   ;
vlinstmt
   : VLIN expression COMMA expression AT expression
   ;
onerrstmt
   : ONERR GOTO linenumber
   ;
prstmt
   : PRNUMBER NUMBER
   ;
instmt
   : INNUMBER NUMBER
   ;
storestmt
   : STORE vardecl
   ;
recallstmt
   : RECALL vardecl
   ;
liststmt
   : LIST expression?
   ;
popstmt
   : POP (expression COMMA expression)?
   ;
amptstmt
   : AMPERSAND expression
   ;
includestmt
   : INCLUDE expression
   ;
endstmt
   : END
   ;
returnstmt
   : RETURN
   ;
restorestmt
   : RESTORE
   ;
number
   :  ('+' | '-')? (NUMBER | FLOAT)
   ;
func_
   : STRINGLITERAL
   | number
   | tabfunc
   | vardecl
   | chrfunc
   | sqrfunc
   | lenfunc
   | strfunc
   | ascfunc
   | scrnfunc
   | midfunc
   | pdlfunc
   | peekfunc
   | intfunc
   | spcfunc
   | frefunc
   | posfunc
   | usrfunc
   | leftfunc
   | valfunc
   | rightfunc
   | fnfunc
   | sinfunc
   | cosfunc
   | tanfunc
   | atnfunc
   | rndfunc
   | sgnfunc
   | expfunc
   | logfunc
   | absfunc
   | LPAREN expression RPAREN
   ;
signExpression
   : NOT? (PLUS | MINUS)? func_
   ;
exponentExpression
   : signExpression (EXPONENT signExpression)*
   ;
multiplyingExpression
   : exponentExpression ((TIMES | DIV) exponentExpression)*
   ;
addingExpression
   : multiplyingExpression ((PLUS | MINUS) multiplyingExpression)*
   ;
relationalExpression
   : addingExpression (relop addingExpression)?
   ;
expression
   : func_
   | relationalExpression ((AND | OR) relationalExpression)*
   ;
var_
   : varname varsuffix?
   ;
varname
   : LETTERS (LETTERS | NUMBER)*
   ;
varsuffix
   : DOLLAR | PERCENT
   ;
varlist
   : vardecl (COMMA vardecl)*
   ;
exprlist
   : expression (COMMA expression)*
   ;
sqrfunc
   : SQR LPAREN expression RPAREN
   ;
chrfunc
   : CHR LPAREN expression RPAREN
   ;
lenfunc
   : LEN LPAREN expression RPAREN
   ;
ascfunc
   : ASC LPAREN expression RPAREN
   ;
midfunc
   : MID LPAREN expression COMMA expression COMMA expression RPAREN
   ;
pdlfunc
   : PDL LPAREN expression RPAREN
   ;
peekfunc
   : PEEK LPAREN expression RPAREN
   ;
intfunc
   : INTF LPAREN expression RPAREN
   ;
spcfunc
   : SPC LPAREN expression RPAREN
   ;
frefunc
   : FRE LPAREN expression RPAREN
   ;
posfunc
   : POS LPAREN expression RPAREN
   ;
usrfunc
   : USR LPAREN expression RPAREN
   ;
leftfunc
   : LEFT LPAREN expression COMMA expression RPAREN
   ;
rightfunc
   : RIGHT LPAREN expression COMMA expression RPAREN
   ;
strfunc
   : STR LPAREN expression RPAREN
   ;
fnfunc
   : FN var_ LPAREN expression RPAREN
   ;
valfunc
   : VAL LPAREN expression RPAREN
   ;
scrnfunc
   : SCRN LPAREN expression COMMA expression RPAREN
   ;
sinfunc
   : SIN LPAREN expression RPAREN
   ;
cosfunc
   : COS LPAREN expression RPAREN
   ;
tanfunc
   : TAN LPAREN expression RPAREN
   ;
atnfunc
   : ATN LPAREN expression RPAREN
   ;
rndfunc
   : RND LPAREN expression RPAREN
   ;
sgnfunc
   : SGN LPAREN expression RPAREN
   ;
expfunc
   : EXP LPAREN expression RPAREN
   ;
logfunc
   : LOG LPAREN expression RPAREN
   ;
absfunc
   : ABS LPAREN expression RPAREN
   ;
tabfunc
   : TAB LPAREN expression RPAREN
   ;
DOLLAR
   : '$'
   ;
PERCENT
   : '%'
   ;
RETURN
   : 'RETURN'
   ;
PRINT
   : 'PRINT'
   ;
GOTO
   : 'GOTO'
   ;
GOSUB
   : 'GOSUB'
   ;
IF
   : 'IF'
   ;
NEXT
   : 'NEXT'
   ;
THEN
   : 'THEN'
   ;

REM
   : 'REM'
   ;

CHR
   : 'CHR$'
   ;

MID
   : 'MID$'
   ;

LEFT
   : 'LEFT$'
   ;

RIGHT
   : 'RIGHT$'
   ;

STR
   : 'STR$'
   ;

LPAREN
   : '('
   ;

RPAREN
   : ')'
   ;

PLUS
   : '+'
   ;

MINUS
   : '-'
   ;

TIMES
   : '*'
   ;

DIV
   : '/'
   ;

CLEAR
   : 'CLEAR'
   ;

GTE
   : '>: '
   ;

LTE
   : '<: '
   ;

GT
   : '>'
   ;

LT
   : '<'
   ;

COMMA
   : ','
   ;

LIST
   : 'LIST'
   ;

RUN
   : 'RUN'
   ;

END
   : 'END'
   ;

LET
   : 'LET'
   ;

EQ
   : '='
   ;

FOR
   : 'FOR'
   ;

TO
   : 'TO'
   ;

STEP
   : 'STEP'
   ;

INPUT
   : 'INPUT'
   ;

SEMICOLON
   : ';'
   ;

DIM
   : 'DIM'
   ;

SQR
   : 'SQR'
   ;

COLON
   : ':'
   ;

TEXT
   : 'TEXT'
   ;

HGR
   : 'HGR'
   ;

HGR2
   : 'HGR2'
   ;

LEN
   : 'LEN'
   ;

CALL
   : 'CALL'
   ;

ASC
   : 'ASC'
   ;

HPLOT
   : 'HPLOT'
   ;

VPLOT
   : 'VPLOT'
   ;

PRNUMBER
   : 'PR#'
   ;

INNUMBER
   : 'IN#'
   ;

VTAB
   : 'VTAB'
   ;

HTAB
   : 'HTAB'
   ;

HOME
   : 'HOME'
   ;

ON
   : 'ON'
   ;

PDL
   : 'PDL'
   ;

PLOT
   : 'PLOT'
   ;

PEEK
   : 'PEEK'
   ;

POKE
   : 'POKE'
   ;

INTF
   : 'INT'
   ;

STOP
   : 'STOP'
   ;

HIMEM
   : 'HIMEM'
   ;

LOMEM
   : 'LOMEM'
   ;

FLASH
   : 'FLASH'
   ;

INVERSE
   : 'INVERSE'
   ;

NORMAL
   : 'NORMAL'
   ;

ONERR
   : 'ONERR'
   ;

SPC
   : 'SPC'
   ;

FRE
   : 'FRE'
   ;

POS
   : 'POS'
   ;

USR
   : 'USR'
   ;

TRACE
   : 'TRACE'
   ;

NOTRACE
   : 'NOTRACE'
   ;

AND
   : 'AND'
   ;

OR
   : 'OR'
   ;

DATA
   : 'DATA'
   ;

WAIT
   : 'WAIT'
   ;

READ
   : 'READ'
   ;

XDRAW
   : 'XDRAW'
   ;

DRAW
   : 'DRAW'
   ;

AT
   : 'AT'
   ;

DEF
   : 'DEF'
   ;

FN
   : 'FN'
   ;

VAL
   : 'VAL'
   ;

TAB
   : 'TAB'
   ;

SPEED
   : 'SPEED'
   ;

ROT
   : 'ROT'
   ;

SCALE
   : 'SCALE'
   ;

COLOR
   : 'COLOR'
   ;

HCOLOR
   : 'HCOLOR'
   ;

HLIN
   : 'HLIN'
   ;

VLIN
   : 'VLIN'
   ;

SCRN
   : 'SCRN'
   ;

POP
   : 'POP'
   ;

SHLOAD
   : 'SHLOAD'
   ;

SIN
   : 'SIN'
   ;

COS
   : 'COS'
   ;

TAN
   : 'TAN'
   ;

ATN
   : 'ATN'
   ;

RND
   : 'RND'
   ;

SGN
   : 'SGN'
   ;

EXP
   : 'EXP'
   ;

LOG
   : 'LOG'
   ;

ABS
   : 'ABS'
   ;

STORE
   : 'STORE'
   ;

RECALL
   : 'RECALL'
   ;

GET
   : 'GET'
   ;

EXPONENT
   : '^'
   ;

AMPERSAND
   : '&'
   ;

GR
   : 'GR'
   ;

NOT
   : 'NOT'
   ;

RESTORE
   : 'RESTORE'
   ;

SAVE
   : 'SAVE'
   ;

LOAD
   : 'LOAD'
   ;

INCLUDE
   : 'INCLUDE'
   ;

CLS
   : 'CLS'
   ;

COMMENT
   : REM ~ [\r\n]*
   ;

STRINGLITERAL
   : '"' ~ ["\r\n]* '"'
   ;

LETTERS
   : ('A' .. 'Z') +
   ;

NUMBER
   : ('0' .. '9') + ('E' NUMBER)*
   ;

FLOAT
   : ('0' .. '9')* '.' ('0' .. '9') + ('E' ('0' .. '9') +)*
   ;

WS
   : [ \t] + -> channel (HIDDEN)
;

NEWLINE
  : '\r'? '\n'
  ;