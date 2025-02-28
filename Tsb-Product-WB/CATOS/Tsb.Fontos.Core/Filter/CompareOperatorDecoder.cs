#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2011 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
* 
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2011.05.06    Jindols 1.0	First release.
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Filter.Type;
using Tsb.Fontos.Core.Log;
using System.Text.RegularExpressions;
using Tsb.Fontos.Core.Filter.Item;

namespace Tsb.Fontos.Core.Filter
{
    public class CompareOperatorDecoder : TsbBaseObject
    {
        #region READONLY AREA *************************************
        private readonly string WILD_CARD_PERCENT = "%";
        private readonly string WILD_CARD_PERCENT_REGEX = ".*";
        private readonly char WILD_CARD_UNDERBAR = '_';
        private readonly string WILD_CARD_UNDERBAR_REGEX = ".";
        private readonly string MATCH_END_CHARACTER_REGEX = "$";
        private readonly string MATCH_LENGTH_PREFIX_REGEX = "(?=^.{";
        private readonly string MATCH_LENGTH_SUFFIX_REGEX = "}$)";
        #endregion

        #region FIELD AREA ***************************************
        private Dictionary<TypeCode, Dictionary<ConditionalOpType, LambdaExpression>> _compareOpDictionary;
        private Dictionary<string, FilterItem> _regexPatternDictionary;
        private static volatile CompareOperatorDecoder _instance;
        private static object syncRoot = new Object();
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Initializes compare operation dictionaries for three supported types of values: char, string, int, float, double and bool. 
        /// </summary>
        private CompareOperatorDecoder()
        {
            try
            {
                this.ObjectID = "GNR-FTDW-Find-CompareOperatorDecoder";

                _compareOpDictionary = new Dictionary<TypeCode, Dictionary<ConditionalOpType, LambdaExpression>>();
                _regexPatternDictionary = new Dictionary<string, FilterItem>();
                this.CreateCharExpression();
                this.CreateStringExpression();
                this.CreateSingleExpression();
                this.CreateDoubleExpression();
                this.CreateInt64Expression();
                this.CreateInt32Expression();
                this.CreateInt16Expression();
                this.CreateBoolExpression();
                this.CreateByteExpression();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Returns a reference to the current CompareOperatorDecoder object for the application
        /// </summary>
        /// <returns>A reference to the current CompareOperatorDecoder object</returns>
        public static CompareOperatorDecoder GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new CompareOperatorDecoder();
                    }
                }
            }

            return _instance;
        }
        #endregion

        #region Create Operator Expression METHOD AREA **************************************
        /// <summary>
        /// String Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateStringExpression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> stringOps = new Dictionary<ConditionalOpType, LambdaExpression>();

                this.AddStringPredicate(stringOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                this.AddStringPredicate(stringOps, ConditionalOpType.NOT_EQUAL, (x, y) => x != y);
                this.AddStringPredicate(stringOps, ConditionalOpType.STARTS_WITH, (x, y) => x.StartsWith(y));
                this.AddStringPredicate(stringOps, ConditionalOpType.ENDS_WITH, (x, y) => x.EndsWith(y));
                this.AddStringPredicate(stringOps, ConditionalOpType.CONTAINS, (x, y) => x.Contains(y));
                this.AddStringPredicate(stringOps, ConditionalOpType.STARTS_WITH_TO_UPPER, (x, y) => x.ToUpper().StartsWith(y.ToUpper()));
                this.AddStringPredicate(stringOps, ConditionalOpType.ENDS_WITH_TO_UPPER, (x, y) => x.ToUpper().EndsWith(y.ToUpper()));
                this.AddStringPredicate(stringOps, ConditionalOpType.CONTAINS_TO_UPPER, (x, y) => x.ToUpper().Contains(y.ToUpper()));

                this.AddStringPredicate(stringOps, ConditionalOpType.STARTS_WITH_TO_LOWER, (x, y) => x.ToLower().StartsWith(y.ToUpper()));
                this.AddStringPredicate(stringOps, ConditionalOpType.ENDS_WITH_TO_LOWER, (x, y) => x.ToLower().EndsWith(y.ToUpper()));
                this.AddStringPredicate(stringOps, ConditionalOpType.CONTAINS_TO_LOWER, (x, y) => x.ToLower().Contains(y.ToUpper()));

                this.AddStringPredicate(stringOps, ConditionalOpType.NULL, (x, y) => x == null);
                this.AddStringPredicate(stringOps, ConditionalOpType.NOT_NULL, (x, y) => x != null);
                
                this.AddStringPredicate(stringOps, ConditionalOpType.LIKE, (x, y) => this.IsMatchedWithRegexPattern(x, y));
                this.AddStringPredicate(stringOps, ConditionalOpType.NOT_LIKE, (x, y) => !this.IsMatchedWithRegexPattern(x, y));
                this.AddStringPredicate(stringOps, ConditionalOpType.NULL_OR_EMPTY, (x, y) => string.IsNullOrEmpty(x));

                this.AddStringPredicate(stringOps, ConditionalOpType.TEXT_LENGTH_EQUAL, (x, y) => x.Length == Convert.ToInt32(y));
                this.AddStringPredicate(stringOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN, (x, y) => x.Length < Convert.ToInt32(y));
                this.AddStringPredicate(stringOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN_OR_EQUAL, (x, y) => x.Length <= Convert.ToInt32(y));
                this.AddStringPredicate(stringOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN, (x, y) => x.Length > Convert.ToInt32(y));
                this.AddStringPredicate(stringOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN_OR_EQUAL, (x, y) => x.Length >= Convert.ToInt32(y));

                _compareOpDictionary.Add(TypeCode.String, stringOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Checks if parttern is to retrive for regex.
        /// </summary>
        /// <param name="patternList"></param>
        /// <returns></returns>
        protected bool HaveRetrivingPattern(List<string> patternList)
        {
            bool haveRetrivingPattern = false;

            try
            {
                if (patternList != null &&
                    patternList.Count == 1 &&
                    patternList[0].Length > 2 &&
                    patternList[0].EndsWith(this.WILD_CARD_PERCENT_REGEX) &&
                    patternList[0].StartsWith(this.WILD_CARD_PERCENT_REGEX) == false)
                {
                    patternList[0] = patternList[0].Replace(this.WILD_CARD_PERCENT_REGEX, this.MATCH_END_CHARACTER_REGEX);
                    haveRetrivingPattern = true;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                return haveRetrivingPattern;
            }

            return haveRetrivingPattern;
        }

        /// <summary>
        /// Checks if input is matched with regex pattern.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="patternList"></param>
        /// <param name="isLIkeFilter"></param>
        /// <returns></returns>
        private bool IsMatchedWithRegexPattern(string input, string operand)
        {
            bool isMached = false;
            string strInput = string.Empty;
            FilterItem filterItem = null;
            List<string> patternList = null;
            bool doRetrive = false;

            try
            {
                if (this._regexPatternDictionary.ContainsKey(operand))
                {
                    filterItem = _regexPatternDictionary[operand];
                }
                else
                {
                    filterItem = new FilterItem();
                    filterItem.PatternList = this.GetRegexPatternList(operand);
                    filterItem.DoRetrive = this.HaveRetrivingPattern(patternList);
                    this._regexPatternDictionary.Add(operand, filterItem);
                }

                patternList = filterItem.PatternList;
                doRetrive = filterItem.DoRetrive;

                if (patternList != null && input != null)
                {
                    if (doRetrive && input.Length >= patternList[0].Length)
                    {
                        strInput = input.Substring(0, patternList[0].Length - 1);
                    }
                    else
                    {
                        strInput = input;
                    }

                    for (int i = 0; i < patternList.Count; i++)
                    {
                        if (i == 1 && isMached == false)
                        {
                            break;
                        }

                        if (Regex.IsMatch(strInput, patternList[i]))
                        {
                            isMached = true;
                        }
                        else
                        {
                            isMached = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                return isMached;
            }

            return isMached;
        }

        /// <summary>
        /// Gets regex pattern list.
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        private List<string> GetRegexPatternList(string operand)
        {
            string pattern = null;
            List<string> patternLIst = null;

            try
            {
                char[] spilitOperand = operand.ToCharArray();
                patternLIst = new List<string>();

                for (int i = 0; i < spilitOperand.Length; i++)
                {
                    if (spilitOperand[i].Equals(this.WILD_CARD_UNDERBAR))
                    {
                        pattern += this.WILD_CARD_UNDERBAR_REGEX;
                    }
                    else
                    {
                        pattern += spilitOperand[i];
                    }
                }

                if (operand.Contains(this.WILD_CARD_PERCENT))
                {
                    pattern = pattern.Replace(this.WILD_CARD_PERCENT, this.WILD_CARD_PERCENT_REGEX);
                }
                else if (operand.Contains(this.WILD_CARD_UNDERBAR) == false)
                {
                    pattern = this.WILD_CARD_PERCENT_REGEX + pattern + this.WILD_CARD_PERCENT_REGEX;
                }

                patternLIst.Add(pattern);

                if (operand.Contains(this.WILD_CARD_UNDERBAR) && operand.Contains(this.WILD_CARD_PERCENT) == false)
                {
                    pattern = this.MATCH_LENGTH_PREFIX_REGEX + operand.Length + this.MATCH_LENGTH_SUFFIX_REGEX;
                    patternLIst[0] += this.MATCH_END_CHARACTER_REGEX;
                    patternLIst.Add(pattern);
                }
                else if (operand.Contains(this.WILD_CARD_UNDERBAR.ToString()))
                {
                    if (operand.EndsWith(this.WILD_CARD_PERCENT) == false)
                    {
                        patternLIst[0] += this.MATCH_END_CHARACTER_REGEX;
                    }
                }
            }
            catch (Exception ex)
            {
                patternLIst = null;
                GeneralLogger.Error(ex);
            }

            return patternLIst;
        }

        /// <summary>
        /// Single(float) Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateSingleExpression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> floatOps = new Dictionary<ConditionalOpType, LambdaExpression>();
                this.AddSinglePredicate(floatOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                this.AddSinglePredicate(floatOps, ConditionalOpType.NOT_EQUAL, (x, y) => x != y);
                this.AddSinglePredicate(floatOps, ConditionalOpType.GREATER_THAN, (x, y) => x > y);
                this.AddSinglePredicate(floatOps, ConditionalOpType.GREATER_THAN_OR_EQUAL, (x, y) => x >= y);
                this.AddSinglePredicate(floatOps, ConditionalOpType.LESS_THAN, (x, y) => x < y);
                this.AddSinglePredicate(floatOps, ConditionalOpType.LESS_THAN_OR_EQUAL, (x, y) => x <= y);

                this.AddSinglePredicate(floatOps, ConditionalOpType.TEXT_LENGTH_EQUAL, (x, y) => x.ToString().Length == Convert.ToInt32(y));
                this.AddSinglePredicate(floatOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN, (x, y) => x.ToString().Length < Convert.ToInt32(y));
                this.AddSinglePredicate(floatOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN_OR_EQUAL, (x, y) => x.ToString().Length <= Convert.ToInt32(y));
                this.AddSinglePredicate(floatOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN, (x, y) => x.ToString().Length > Convert.ToInt32(y));
                this.AddSinglePredicate(floatOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN_OR_EQUAL, (x, y) => x.ToString().Length >= Convert.ToInt32(y));

                _compareOpDictionary.Add(TypeCode.Single, floatOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        /// <summary>
        /// Double Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateDoubleExpression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> doubleOps = new Dictionary<ConditionalOpType, LambdaExpression>();
                this.AddIntPredicate(doubleOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                this.AddIntPredicate(doubleOps, ConditionalOpType.NOT_EQUAL, (x, y) => x != y);
                this.AddIntPredicate(doubleOps, ConditionalOpType.GREATER_THAN, (x, y) => x > y);
                this.AddIntPredicate(doubleOps, ConditionalOpType.GREATER_THAN_OR_EQUAL, (x, y) => x >= y);
                this.AddIntPredicate(doubleOps, ConditionalOpType.LESS_THAN, (x, y) => x < y);
                this.AddIntPredicate(doubleOps, ConditionalOpType.LESS_THAN_OR_EQUAL, (x, y) => x <= y);

                this.AddIntPredicate(doubleOps, ConditionalOpType.TEXT_LENGTH_EQUAL, (x, y) => x.ToString().Length == Convert.ToInt32(y));
                this.AddIntPredicate(doubleOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN, (x, y) => x.ToString().Length < Convert.ToInt32(y));
                this.AddIntPredicate(doubleOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN_OR_EQUAL, (x, y) => x.ToString().Length <= Convert.ToInt32(y));
                this.AddIntPredicate(doubleOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN, (x, y) => x.ToString().Length > Convert.ToInt32(y));
                this.AddIntPredicate(doubleOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN_OR_EQUAL, (x, y) => x.ToString().Length >= Convert.ToInt32(y));

                _compareOpDictionary.Add(TypeCode.Double, doubleOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }        
        }
        /// <summary>
        /// Int64 Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateInt64Expression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> intOps = new Dictionary<ConditionalOpType, LambdaExpression>();
                this.AddIntPredicate(intOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                this.AddIntPredicate(intOps, ConditionalOpType.NOT_EQUAL, (x, y) => x != y);
                this.AddIntPredicate(intOps, ConditionalOpType.GREATER_THAN, (x, y) => x > y);
                this.AddIntPredicate(intOps, ConditionalOpType.GREATER_THAN_OR_EQUAL, (x, y) => x >= y);
                this.AddIntPredicate(intOps, ConditionalOpType.LESS_THAN, (x, y) => x < y);
                this.AddIntPredicate(intOps, ConditionalOpType.LESS_THAN_OR_EQUAL, (x, y) => x <= y);

                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_EQUAL, (x, y) => x.ToString().Length == Convert.ToInt32(y));
                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN, (x, y) => x.ToString().Length < Convert.ToInt32(y));
                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN_OR_EQUAL, (x, y) => x.ToString().Length <= Convert.ToInt32(y));
                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN, (x, y) => x.ToString().Length > Convert.ToInt32(y));
                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN_OR_EQUAL, (x, y) => x.ToString().Length >= Convert.ToInt32(y));

                _compareOpDictionary.Add(TypeCode.Int64, intOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        /// <summary>
        /// Int32 Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateInt32Expression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> intOps = new Dictionary<ConditionalOpType, LambdaExpression>();
                this.AddIntPredicate(intOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                this.AddIntPredicate(intOps, ConditionalOpType.NOT_EQUAL, (x, y) => x != y);
                this.AddIntPredicate(intOps, ConditionalOpType.GREATER_THAN, (x, y) => x > y);
                this.AddIntPredicate(intOps, ConditionalOpType.GREATER_THAN_OR_EQUAL, (x, y) => x >= y);
                this.AddIntPredicate(intOps, ConditionalOpType.LESS_THAN, (x, y) => x < y);
                this.AddIntPredicate(intOps, ConditionalOpType.LESS_THAN_OR_EQUAL, (x, y) => x <= y);

                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_EQUAL, (x, y) => x.ToString().Length == Convert.ToInt32(y));
                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN, (x, y) => x.ToString().Length < Convert.ToInt32(y));
                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN_OR_EQUAL, (x, y) => x.ToString().Length <= Convert.ToInt32(y));
                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN, (x, y) => x.ToString().Length > Convert.ToInt32(y));
                this.AddIntPredicate(intOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN_OR_EQUAL, (x, y) => x.ToString().Length >= Convert.ToInt32(y));

                _compareOpDictionary.Add(TypeCode.Int32, intOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        /// <summary>
        /// Int16 Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateInt16Expression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> shortOps = new Dictionary<ConditionalOpType, LambdaExpression>();
                this.AddShortPredicate(shortOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                this.AddShortPredicate(shortOps, ConditionalOpType.NOT_EQUAL, (x, y) => x != y);
                this.AddShortPredicate(shortOps, ConditionalOpType.GREATER_THAN, (x, y) => x > y);
                this.AddShortPredicate(shortOps, ConditionalOpType.GREATER_THAN_OR_EQUAL, (x, y) => x >= y);
                this.AddShortPredicate(shortOps, ConditionalOpType.LESS_THAN, (x, y) => x < y);
                this.AddShortPredicate(shortOps, ConditionalOpType.LESS_THAN_OR_EQUAL, (x, y) => x <= y);

                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_EQUAL, (x, y) => x.ToString().Length == Convert.ToInt32(y));
                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN, (x, y) => x.ToString().Length < Convert.ToInt32(y));
                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN_OR_EQUAL, (x, y) => x.ToString().Length <= Convert.ToInt32(y));
                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN, (x, y) => x.ToString().Length > Convert.ToInt32(y));
                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN_OR_EQUAL, (x, y) => x.ToString().Length >= Convert.ToInt32(y));

                _compareOpDictionary.Add(TypeCode.Int16, shortOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Byte Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateByteExpression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> shortOps = new Dictionary<ConditionalOpType, LambdaExpression>();
                this.AddShortPredicate(shortOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                this.AddShortPredicate(shortOps, ConditionalOpType.NOT_EQUAL, (x, y) => x != y);
                this.AddShortPredicate(shortOps, ConditionalOpType.GREATER_THAN, (x, y) => x > y);
                this.AddShortPredicate(shortOps, ConditionalOpType.GREATER_THAN_OR_EQUAL, (x, y) => x >= y);
                this.AddShortPredicate(shortOps, ConditionalOpType.LESS_THAN, (x, y) => x < y);
                this.AddShortPredicate(shortOps, ConditionalOpType.LESS_THAN_OR_EQUAL, (x, y) => x <= y);

                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_EQUAL, (x, y) => x.ToString().Length == Convert.ToInt32(y));
                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN, (x, y) => x.ToString().Length < Convert.ToInt32(y));
                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_LESS_THAN_OR_EQUAL, (x, y) => x.ToString().Length <= Convert.ToInt32(y));
                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN, (x, y) => x.ToString().Length > Convert.ToInt32(y));
                this.AddShortPredicate(shortOps, ConditionalOpType.TEXT_LENGTH_GREATER_THAN_OR_EQUAL, (x, y) => x.ToString().Length >= Convert.ToInt32(y));

                _compareOpDictionary.Add(TypeCode.Byte, shortOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        /// <summary>
        /// Bool Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateBoolExpression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> boolOps = new Dictionary<ConditionalOpType, LambdaExpression>();
                this.AddBoolPredicate(boolOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                this.AddBoolPredicate(boolOps, ConditionalOpType.NOT_EQUAL, (x, y) => x != y);
                _compareOpDictionary.Add(TypeCode.Boolean, boolOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        /// <summary>
        /// Char Type의 비교연산을 위한 람다식 생성
        /// </summary>
        private void CreateCharExpression()
        {
            try
            {
                Dictionary<ConditionalOpType, LambdaExpression> charOps = new Dictionary<ConditionalOpType, LambdaExpression>();
                this.AddBoolPredicate(charOps, ConditionalOpType.EQUAL, (x, y) => x == y);
                _compareOpDictionary.Add(TypeCode.Char, charOps);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Single(float) Type의 비교연산을 위한 람다식 등록
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="opName"></param>
        /// <param name="expr"></param>
        private void AddSinglePredicate(Dictionary<ConditionalOpType, LambdaExpression> dict,
            ConditionalOpType opName,
            Expression<Func<float, float, bool>> expr)
        {
            dict.Add(opName, expr);
        }
        /// <summary>
        /// Double Type의 비교연산을 위한 람다식 등록
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="opName"></param>
        /// <param name="expr"></param>
        private void AddDoublePredicate(Dictionary<ConditionalOpType, LambdaExpression> dict,
            ConditionalOpType opName,
            Expression<Func<double, double, bool>> expr)
        {
            dict.Add(opName, expr);
        }
        /// <summary>
        /// Int Type의 비교연산을 위한 람다식 등록
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="opName"></param>
        /// <param name="expr"></param>
        private void AddIntPredicate(Dictionary<ConditionalOpType, LambdaExpression> dict,
            ConditionalOpType opName,
            Expression<Func<int, int, bool>> expr)
        {
            dict.Add(opName, expr);
        }
        /// <summary>
        /// Short Type의 비교연산을 위한 람다식 등록
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="opName"></param>
        /// <param name="expr"></param>
        private void AddShortPredicate(Dictionary<ConditionalOpType, LambdaExpression> dict,
           ConditionalOpType opName,
           Expression<Func<short, short, bool>> expr)
        {
            dict.Add(opName, expr);
        }
        /// <summary>
        /// String Type의 비교연산을 위한 람다식 등록
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="opName"></param>
        /// <param name="expr"></param>
        private void AddStringPredicate(Dictionary<ConditionalOpType, LambdaExpression> dict,
            ConditionalOpType opName,
            Expression<Func<string, string, bool>> expr)
        {
            dict.Add(opName, expr);
        }
        /// <summary>
        /// Bool Type의 비교연산을 위한 람다식 등록
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="opName"></param>
        /// <param name="expr"></param>
        private void AddBoolPredicate(Dictionary<ConditionalOpType, LambdaExpression> dict,
           ConditionalOpType opName,
           Expression<Func<bool, bool, bool>> expr)
        {
            dict.Add(opName, expr);
        }
        /// <summary>
        /// 입력된 Type에 해당하는 비교 Key값 반환
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<ConditionalOpType> GetCompareOpNames(System.Type type)
        {
            Dictionary<ConditionalOpType, LambdaExpression> dict;
            if (!_compareOpDictionary.TryGetValue(System.Type.GetTypeCode(type), out dict))
                return null;
            return dict.Keys;
        }
        /// <summary>
        /// 비교할 Data Type 및 연사자 종류에 해당하는 람다식을 반환한다.
        /// </summary>
        /// <param name="valueType">비교할 Data Type</param>
        /// <param name="compareOp">비교연산자 종류</param>
        /// <returns></returns>
        public LambdaExpression Decode(System.Type valueType, ConditionalOpType compareOp)
        {
            if (valueType == null)
                throw new ArgumentNullException("valueType");

            Dictionary<ConditionalOpType, LambdaExpression> dict;
            if (!_compareOpDictionary.TryGetValue(System.Type.GetTypeCode(valueType), out dict))
                //MSG_FTCO_00162 : Decoder dictionary doesn't contain compare operations over type '{0}'.
                throw new ArgumentOutOfRangeException(MessageBuilder.BuildMessage("MSG_FTCO_00162"
                       , DefaultMessage.NON_REG_WRD + valueType));

            LambdaExpression ret;
            if (!dict.TryGetValue(compareOp, out ret))
                //MSG_FTCO_00163 : Decoder dictionarydoesn't contain compare operation '{0}' over type '{1}'.
                throw new ArgumentOutOfRangeException(MessageBuilder.BuildMessage("MSG_FTCO_00163"
                        , DefaultMessage.NON_REG_WRD + compareOp
                        , DefaultMessage.NON_REG_WRD + valueType));

            return ret;
        }
        #endregion
        
    }
}
