//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Basic.g4 by ANTLR 4.13.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace JesseFreeman.BasicInterpreter.AntlrGenerated {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="BasicParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.CLSCompliant(false)]
public interface IBasicVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.prog"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProg([NotNull] BasicParser.ProgContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] BasicParser.LineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.lineWithoutNumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLineWithoutNumber([NotNull] BasicParser.LineWithoutNumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.amperoper"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAmperoper([NotNull] BasicParser.AmperoperContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.linenumber"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLinenumber([NotNull] BasicParser.LinenumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.amprstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAmprstmt([NotNull] BasicParser.AmprstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] BasicParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.vardecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVardecl([NotNull] BasicParser.VardeclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.printstmt1"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrintstmt1([NotNull] BasicParser.Printstmt1Context context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.getstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGetstmt([NotNull] BasicParser.GetstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.letstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLetstmt([NotNull] BasicParser.LetstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.variableassignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableassignment([NotNull] BasicParser.VariableassignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.stringVarDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringVarDecl([NotNull] BasicParser.StringVarDeclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelop([NotNull] BasicParser.RelopContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.neq"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNeq([NotNull] BasicParser.NeqContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.ifstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfstmt([NotNull] BasicParser.IfstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.forstmt1"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForstmt1([NotNull] BasicParser.Forstmt1Context context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.forstmt2"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForstmt2([NotNull] BasicParser.Forstmt2Context context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.nextstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNextstmt([NotNull] BasicParser.NextstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.inputstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInputstmt([NotNull] BasicParser.InputstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.readstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReadstmt([NotNull] BasicParser.ReadstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.dimstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDimstmt([NotNull] BasicParser.DimstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.gotostmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGotostmt([NotNull] BasicParser.GotostmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.gosubstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGosubstmt([NotNull] BasicParser.GosubstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.pokestmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPokestmt([NotNull] BasicParser.PokestmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.callstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCallstmt([NotNull] BasicParser.CallstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.hplotstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHplotstmt([NotNull] BasicParser.HplotstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.vplotstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVplotstmt([NotNull] BasicParser.VplotstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.plotstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPlotstmt([NotNull] BasicParser.PlotstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.ongotostmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOngotostmt([NotNull] BasicParser.OngotostmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.ongosubstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOngosubstmt([NotNull] BasicParser.OngosubstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.vtabstmnt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVtabstmnt([NotNull] BasicParser.VtabstmntContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.htabstmnt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHtabstmnt([NotNull] BasicParser.HtabstmntContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.himemstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHimemstmt([NotNull] BasicParser.HimemstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.lomemstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLomemstmt([NotNull] BasicParser.LomemstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.datastmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDatastmt([NotNull] BasicParser.DatastmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.datum"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDatum([NotNull] BasicParser.DatumContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.waitstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWaitstmt([NotNull] BasicParser.WaitstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.xdrawstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitXdrawstmt([NotNull] BasicParser.XdrawstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.drawstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDrawstmt([NotNull] BasicParser.DrawstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.defstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefstmt([NotNull] BasicParser.DefstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.tabstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTabstmt([NotNull] BasicParser.TabstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.speedstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSpeedstmt([NotNull] BasicParser.SpeedstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.rotstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRotstmt([NotNull] BasicParser.RotstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.scalestmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScalestmt([NotNull] BasicParser.ScalestmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.colorstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitColorstmt([NotNull] BasicParser.ColorstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.hcolorstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHcolorstmt([NotNull] BasicParser.HcolorstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.hlinstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHlinstmt([NotNull] BasicParser.HlinstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.vlinstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVlinstmt([NotNull] BasicParser.VlinstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.onerrstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOnerrstmt([NotNull] BasicParser.OnerrstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.prstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrstmt([NotNull] BasicParser.PrstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.instmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInstmt([NotNull] BasicParser.InstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.storestmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStorestmt([NotNull] BasicParser.StorestmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.recallstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRecallstmt([NotNull] BasicParser.RecallstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.liststmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListstmt([NotNull] BasicParser.ListstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.popstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPopstmt([NotNull] BasicParser.PopstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.amptstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAmptstmt([NotNull] BasicParser.AmptstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.includestmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIncludestmt([NotNull] BasicParser.IncludestmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.endstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEndstmt([NotNull] BasicParser.EndstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.returnstmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnstmt([NotNull] BasicParser.ReturnstmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.restorestmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRestorestmt([NotNull] BasicParser.RestorestmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumber([NotNull] BasicParser.NumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.func_"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunc_([NotNull] BasicParser.Func_Context context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.signExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSignExpression([NotNull] BasicParser.SignExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.exponentExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExponentExpression([NotNull] BasicParser.ExponentExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.multiplyingExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplyingExpression([NotNull] BasicParser.MultiplyingExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.addingExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddingExpression([NotNull] BasicParser.AddingExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.relationalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelationalExpression([NotNull] BasicParser.RelationalExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] BasicParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.var_"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar_([NotNull] BasicParser.Var_Context context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.varname"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarname([NotNull] BasicParser.VarnameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.varsuffix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarsuffix([NotNull] BasicParser.VarsuffixContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.varlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarlist([NotNull] BasicParser.VarlistContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.exprlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprlist([NotNull] BasicParser.ExprlistContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.sqrfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSqrfunc([NotNull] BasicParser.SqrfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.chrfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitChrfunc([NotNull] BasicParser.ChrfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.lenfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLenfunc([NotNull] BasicParser.LenfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.ascfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAscfunc([NotNull] BasicParser.AscfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.midfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMidfunc([NotNull] BasicParser.MidfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.pdlfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPdlfunc([NotNull] BasicParser.PdlfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.peekfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPeekfunc([NotNull] BasicParser.PeekfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.intfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntfunc([NotNull] BasicParser.IntfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.spcfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSpcfunc([NotNull] BasicParser.SpcfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.frefunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFrefunc([NotNull] BasicParser.FrefuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.posfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPosfunc([NotNull] BasicParser.PosfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.usrfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUsrfunc([NotNull] BasicParser.UsrfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.leftfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLeftfunc([NotNull] BasicParser.LeftfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.rightfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRightfunc([NotNull] BasicParser.RightfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.strfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStrfunc([NotNull] BasicParser.StrfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.fnfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFnfunc([NotNull] BasicParser.FnfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.valfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValfunc([NotNull] BasicParser.ValfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.scrnfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScrnfunc([NotNull] BasicParser.ScrnfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.sinfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSinfunc([NotNull] BasicParser.SinfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.cosfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCosfunc([NotNull] BasicParser.CosfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.tanfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTanfunc([NotNull] BasicParser.TanfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.atnfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtnfunc([NotNull] BasicParser.AtnfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.rndfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRndfunc([NotNull] BasicParser.RndfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.sgnfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSgnfunc([NotNull] BasicParser.SgnfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.expfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpfunc([NotNull] BasicParser.ExpfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.logfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogfunc([NotNull] BasicParser.LogfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.absfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAbsfunc([NotNull] BasicParser.AbsfuncContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BasicParser.tabfunc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTabfunc([NotNull] BasicParser.TabfuncContext context);
}
} // namespace JesseFreeman.BasicInterpreter.AntlrGenerated
