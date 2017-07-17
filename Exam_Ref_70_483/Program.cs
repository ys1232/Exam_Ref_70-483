using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Ref_70_483
{
    class Program
    {
        static void Main(string[] args)
        {
            #region chapter1
            //Listing1_1.TestThreadMethod();
            //Listing1_2.CallThreadMethod();
            //Listing1_3.CallThreadMethod();
            //Listing1_4.stopThread();
            //Listing1_5.ThreadStaticAttribute();
            //Listing1_6.ThreadLocal();
            //new Listing1_7();
            //Listing1_8.newTask();
            //Listing1_9.TaskReturnValue();
            //Listing1_10.TaskContinuation();
            //Listing1_11.TaskContinueOverWrite();
            //Listing1_12.ChildTask();
            //Listing1_12_WithFactory.ChildTask();
            //Listing1_13.UseTaskFactory();
            //Listing1_14.TaskWaitAll_Test();
            //Listing1_15.waitAny_Test();
            //Listing1_16.Parallel_Loop();
            //Listing1_17.TerminateParallelLoop_Test();
            //Listing1_18.Test_Task_Async_And_Await();
            //Listing1_22.Test_AsParallel();
            //Listing1_24.Test_AsParallel_Ordered();
            //Listing1_25.Test_Parallel_AsSequential();
            //Listing1_26.Test_Parallel_ForAll();
            //Listing1_27.Test_Parallel_AggregateException();

            // ------- concurrent collections --------------
            //Listing1_28.Test_BlockingCollection();
            //Listing1_29.Test_GetConsumingEnumerable();
            //Listing1_30.Test_ConcurrentBag();
            //Listing1_31.Test_Enumerating_In_ConcurrentBag();
            //Listing1_32.Test_ConcurrentStack();
            //Listing1_33.Test_ConcurrentQueue();
            //Listing1_34.Test_ConcurrentDictionary();

            //----- 1-2 Manage MultiThreading          ---> how to sync shared source and cancle a task
            //Listing1_35.Test_MultiThread_Accesing_SharedData();
            //Listing1_36.Test_MultiThread_Accesing_SharedData_WithLock();
            //Listing1_37.Test_Create_DeadLock();
            //Listing1_37_2.Test_Create_DeadLock();
            //Listing1_38.Test_GeneratedCode_for_LockStatement();  // 1-38 will not print anything out...
            //Listing1_39.Test_Multithread_Poteintial_Problem();
            //Listing1_40.Test_InterLockedClass_AtomicOperation();
            //Listing1_41.Test_NonAtomic_Operations();
            //Listing1_42.Test_CancellationToken();
            //Listing1_43.Test_Throw_CanceledException();
            //Listing1_44.Test_Add_Continuation_For_CanceledTasks();
            //Listing1_45.Test_timeout_on_Task();

            // ----------- 1-3 Implemnet program flow
            //Listing1_46_51.Test_Equality_Operation();
            //Listing1_46_51.Test_Boolean_Or_Operator();
            //Listing1_46_51.Test_OrShortCircuit();
            //Listing1_46_51.Test_And_Operator();
            //Listing1_46_51.Test_Short_Circuiting_And_Operator();
            //Listing1_46_51.Test_XOR_Operator();

            //Listing1_52_63.Test_52_BasicIF();
            //Listing1_52_63.Test_53_IfWithCodeBlock();
            //Listing1_52_63.Test_54_CodeBlocks_ANd_Scoping();
            //Listing1_52_63.Test_55_ElseStatement();
            //Listing1_52_63.Test_56_Multiple_IfElse();
            //Listing1_52_63.Test_57_Nested_If_Statement();
            //Listing1_52_63.Test_58_Null_Coalescing_Operator();
            //Listing1_52_63.Test_59_Nesting_Null_Coalescing_Operator();
            //Listing1_52_63.Test_60_Conditional_Operator();
            //Listing1_52_63.Test_61_Check('A');
            //Listing1_52_63.Test_62_CheckWithSwitch('y');
            //Listing1_52_63.Test_63_GoTo_Within_switch();

            //Listing1_64_74.Test_64_Basic_ForLoop();
            //Listing1_64_74.Test_65_ForLoop_With_MultipleVariables();
            //Listing1_64_74.Test_66_ForLoop_With_Custom_Increment();
            //Listing1_64_74.Test_67_ForLoop_with_Break();
            //Listing1_64_74.Test_68_ForLoop_Witn_Continue();
            //Listing1_64_74.Test_69_While_Statement();
            //Listing1_64_74.Test_70_DoWhile_Loop();
            //Listing1_64_74.Test_71_ForeachLoop();
            //Listing1_64_74.Test_72_Changing_Items_In_a_Foreach();
            //Listing1_64_74.Test_73_Complier_generated_Code_For_ForeachLoop();
            //Listing1_64_74.Test_74_Goto_Statement_With_Label();

            // ------------ 1-4 CREATE AND IMPLEMENT EVENTS AND CALLBACKS
            //C1_Listing75.UseDelegate();
            //C1_Listing76.Multicast();
            //C1_Listing77.Test_CovarianceDel();
            //C1_Listing78.Test_Contravariance();
            //C1_Listing79.Test_LambdaFunction();         
            //C1_Listing80.Test_Lambda_Function_With_MultipleStatement();
            //C1_Listing81.Test_BuildIn_Delegate_and_Action_Type();
            //C1_Listing82.CreateAndRaise();
            //C1_Listing82_version2.CreateAndRaise();
            //C1_Listing84.CreateAndRaise();
            //C1_Listing85.CreateAndRaise();
            //C1_Listing86.CreateAndRaise();
            //C1_Listing87.CreateAndRaise();

            // ----------- 1-5 Implement exception handling
            //C1_Listing88.Test_Unhandled_Exception();
            //C1_Listing89.Test_Catch_FormatException();
            //C1_Listing90.Test_Catch_Dif_Exception_Types();
            //C1_Listing91.Test_FinallyBlock();
            //C1_Listing92.Test_Environment_FailFast();
            //C1_Listing93.Test_Inspect_Exception();
            //C1_Listing94.Test_ThrowException();
            //C1_Listing95.Test_ReThrow_Exception();
            //C1_Listing96.Test_Adding_To_Original_Exception();
            //C1_Listing97.Test_Use_ExceptionDispatchInfo();
            #endregion

            #region chapter2
            // -------------------------------chapter 2 ---------------------
            //C2_Listing1.Test_Enum();
            //new C2_Listing2();   // there is not much you can do in this example
            //C2_Listing3.Test_Call_method();
            //C2_Listing4.Test_Creating_Method();
            //C2_Listing5.Test_Passing_A_customer_to_Method();
            //C2_Listing6.Test_Passing_An_Address_to_Method();
            //C2_Listing7.Test_Default_and_Optional_Argument();
            //C2_Listing8.Test_returnValue();
            //C2_Listing9.Test_UsingField();
            //C2_Listing10.Test_Creating_Collection_Class();
            //C2_Listing11.Test_Constructor();
            //C2_Listing12.Test_ConstructorChaining();

            //C2_Listing16.Test_Extension_Method();
            //C2_Listing17.Test_Class_Derive();

            //C2_Listing19.Test_Boxing_Unboxing_Integer();
            //C2_Listing20_21.Test_Implicit_Conversion_Value_Type();
            //C2_Listing20_21.Test_Implicit_Conversion_Reference_Type();
            //C2_Listing22_23.Test_Explicit_Conversion_Value_Type();
            //C2_Listing22_23.Test_Explicit_Conversion_Reference_Type();
            //C2_Listing24_25.Test_User_Defined_Conversion();
            //C2_Listing26.Test_Built_in_Conversion_Function();
            //C2_Listing27.Test_as_And_is_operators();
            //C2_Listing29.Test_Dynamic_Object();

            //C2_Listing31_33.Test_Accesss_Modifier();

            //C2_Listing_36_37.Test_Property();

            //C2_Listing44.Test_Instantiating_Interface();

            //C2_Listing47.Test_Virtual_Class_Extension();
            //C2_Listing48.Test_Hiding_Method_With_New();
            //C2_Listing50_52.Test_Use_Square_Class();
            //C2_Listing53_54.Test_Implement_IComparable_Interface();   // ??? no idea why Listing53_54 doesn't work...
            //C2_Listing55.Test_Enumerator_Iteration();
            //C2_Listing61.Test_Check_Attribute_Defined();
            //C2_Listing62.Test_Getting_Attribute_Instance();
            //C2_Listing63_68.Test_Access_Custom_Attribute();

            //C2_Listing72.Test_Getting_Values_of_a_Field_Via_Reflection();
            //C2_Listing73.Test_execute_method_through_reflection();
            //C2_Listing74_76.Test_Using_CodeDOM();
            //C2_Listing77.Test_Create_Lambda_Function();
            //C2_Listing78.Test_Creating_Expression_Tree();
            //C2_Listing79_81.Test_finalizer();
            //C2_Listing82_83.Test_Dispose();

            //C2_Listing86_88.Test_String_Builder();
            //C2_Listing89_90.Test_StringWriter_StringReader();
            //C2_Listing91_94.Test_Search_For_String();
            //C2_Listing95.Test_enumerating_string();
            //C2_Listing96.Test_Override_ToString_Method();
            //C2_Listing97_98.Test_Display_Formatted_Strings();
            //C2_Listing99.Test_Custom_Format_Method();
            //C2_Listing100.Test_Create_Composite_String_Format();
            #endregion

            #region Chapter3
            // ---------------------------- Chapter 3 ----------------------------
            //C3_Listing4_6.Test_Parse_TryParse();
            //C3_Listing7_8.Test_Convert();
            //C3_Listing9_10.Test_ZipCode();
            //C3_Listing11.Test_Collapse_multiple_Space_with_RegEx();
            //C3_Listing23.Test_SHA256Managed_Hash_method();

            //C3_Listing33.Test_debug_Mode_release_Mode();
            //C3_Listing34_35.Test_Compiler_Directive_IF();
            //C3_Listing37.Test_Warning_Error_Directive();
            //C3_Listing38.Test_Line_Directive();
            //C3_Listing41.Test_Call_Method_Only_In_Debug_Mode();
            //C3_Listing42.Test_Conditional_Attribute();
            //C3_Listing43.Test_DebuggerDisplay();

            //C3_Listing45.Test_Debug_Class();
            //C3_Listing46.Test_TraceSourceClass();
            //C3_Listing47.Test_Configure_TraceListener();
            //C3_Listing49.Test_Writing_Event_Log();
            //C3_Listing50.Test_Read_From_EventLog();
            //C3_Listing51.Test_Write_Data_To_EvntLog();
            //C3_Listing52.Test_StopWatchClass();
            //C3_Listing52.Test_Profiler();
            #endregion


            // ------------------------- Chapter 4 ----------------------
            //C4_Listing2.Test_Create_NewDirectory();
            //C4_Listing3.Test_Delete_Existing_Directory();
            //C4_Listing5.Test_Build_directory_tree();
            //C4_Listing6.Test_Move_Directory();
            //C4_Listing7.Test_Listing_all_Files_in_Directory();
            //C4_Listing8.Test_Delete_File();
            //C4_Listing9_10.Test_Moving_a_File();
            //C4_Listing9_10.Test_Copy_a_File();
            //C4_Listing11_12.Test_Manually_Concatenate_filePath();
            //C4_Listing11_12.Test_Use_PathCombine_Method();
            //C4_Listing13.Test_other_Path_Method();
            //C4_Listing14.Test_use_FileStream();
            //C4_Listing15.Test_StreamWriter();
            //C4_Listing16.Test_FileStream_Decoding();
            //C4_Listing17.Test_TextFile_reading_Content();
            //C4_Listing18.Test_using_GZipStream_compressing_data();
            //C4_Listing19.Test_Buffer_Stream();
            //C4_Listing23.Test_WriteAsync();
            //C4_Listing24.Test_GetStringAsync();
            //await C4_Listing25.ExecuteMultipleRequests();
            //C4_Listing27.Test_SqlConnection();
            //C4_Listing28.Test_SqlConnStrBuilder();
            //C4_Listing29_30.Test_Config_File();

            try
            {
                //C4_Listing32.Test_Async_Select_execution().Wait();
                //C4_Listing33.Test_Async_Select_MultipleResultSets().Wait()
                //C4_Listing34.Test_UpdateRows().Wait();
                //C4_Listing35.Test_InsertWithParameterizedQuery().Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine("There are {0} exceptions", e.InnerExceptions.Count);
                Console.WriteLine("The exception is {0}", e.ToString());

                Console.ReadKey();
            }

            //C4_Listing36.Test_UsingTransactionScope();
            //C4_Listing37_38.Test_EntityFramework();

            //C4_Listing43.Test_XmlReader();
            //C4_Listing44.Test_XmlWriter();
            //C4_Listing45.Test_XMLDocument();
            //C4_Listing46.Test_XPath_Query();

            //------ chapter 4.3
            //C4_Listing48_50.Test_Object_Initializer();
            //C4_Listing51.Test_anonymous_and_Lambda();
            //C4_Listing52.Test_Extension_Method();
            //C4_Listing53.Test_Anonymoust_Type();
            //C4_Listing54.Test_LINQ_select_Query();

            //C4_Listing55_62.Test_Select_Operator();
            //C4_Listing55_62.Test_Where_Opertator();
            //C4_Listing55_62.Test_Orderby_Opertator();
            //C4_Listing55_62.Test_Multiple_From();
            //C4_Listing64_69.Test_Query_XML();
            //C4_Listing64_69.Test_XML_Where_and_OrderBy();
            //C4_Listing64_69.Test_CreateXML_WithXElement();
            //C4_Listing64_69.Test_Update_XML();
            //C4_Listing64_69.Test_Transforming_XML();
            //C4_Listing70.Test_XmlSerializer();
            //C4_Listing71_72.Test_XMLAttribute();
            //C4_Listing73.Test_binary_serialization();
            C4_Listing75.Test_Influencing_binary_serialization();
        }
    }
}
