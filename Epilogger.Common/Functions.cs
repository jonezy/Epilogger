using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epilogger.Common
{

    public static class Functions
    {

        public const string sBase58Alphabet = "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";

        public enum CollectionModes : int
        {
            BeforeEvent=1,
            ActiveEvent=2,
            AfterEvent=3,
            AfterEventArchived=4,
            OnGoing=5
        }

        public static int RandomInt(Random rng, int min, int max)
        {
            return rng.Next(min, max);
        }

        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public static string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        public static long DecodeBase58(string base58StringToExpand)
        {
            long lConverted = 0;
            long lTemporaryNumberConverter = 1;

            while (base58StringToExpand.Length > 0)
            {
                String sCurrentCharacter = base58StringToExpand.Substring(base58StringToExpand.Length - 1);
                lConverted = lConverted + (lTemporaryNumberConverter * sBase58Alphabet.IndexOf(sCurrentCharacter));
                lTemporaryNumberConverter = lTemporaryNumberConverter * sBase58Alphabet.Length;
                base58StringToExpand = base58StringToExpand.Substring(0, base58StringToExpand.Length - 1);
            }

            return lConverted;
        }

        public static String EncodeBase58(UInt32 numberToShorten)
        {
            String sConverted = "";
            Int32 iAlphabetLength = sBase58Alphabet.Length;

            while (numberToShorten > 0)
            {
                long lNumberRemainder = (numberToShorten % iAlphabetLength);
                numberToShorten = Convert.ToUInt32(numberToShorten / iAlphabetLength);
                sConverted = sBase58Alphabet[Convert.ToInt32(lNumberRemainder)] + sConverted;
            }

            return sConverted;
        }








        //REMOVE - This doesn't belong here
        public static Boolean UpdateEventCollectionModes()
        {

            try 
	        {

                //Epilogger.Data.EpiloggerDB db = new Data.EpiloggerDB();

                //var Events = EpiloggerDB.Events.Where(e => e.IsActive);

                //foreach (Data.Event e in Events)
                //{
                //    TimeSpan TimeS = e.StartDateTime - DateTime.UtcNow;

                //    //TODO - Write the rest of this function


                //}
                

                    
                    //Dim CompareToStart As Integer = Date.Compare(DateTime.UtcNow, e.StartDateTime)

                    //Dim EndDateTime As DateTime
                    //If e.EndDateTime.HasValue Then
                    //    EndDateTime = e.EndDateTime
                    //Else
                    //    EndDateTime = DateTime.Parse("2200-01-01 00:00:00")
                    //End If
                    //Dim CompareToEnd As Integer = Date.Compare(DateTime.UtcNow, EndDateTime)


                    //If CompareToStart < 0 Then
                    //    ShouldBeInMode = CollectionModes.BeforeEvent
                    //ElseIf CompareToStart > 0 And CompareToEnd < 0 Then
                    //    ShouldBeInMode = CollectionModes.ActiveEvent

                    //ElseIf CompareToEnd > 0 Then
                    //    'If DateDiff(DateInterval.Day, CDate(e.StartDateTime), Now()) > 14 Then
                    //    '    ShouldBeInMode = CollectionModes.AfterEventArchived
                    //    'Else
                    //    '    ShouldBeInMode = CollectionModes.AfterEventWiki
                    //    'End If

                    //    If DateDiff(DateInterval.Hour, CDate(e.CollectionEndDateTime), DateTime.UtcNow) <= 0 Then
                    //        ShouldBeInMode = CollectionModes.AfterEventWiki
                    //    Else
                    //        ShouldBeInMode = CollectionModes.AfterEventArchived
                    //    End If

                    //End If

                    //If e.CollectionMode <> ShouldBeInMode Then
                    //    e.CollectionMode = ShouldBeInMode
                    //End If
                


                return true;
	        }
	        catch (Exception)
	        {
		        return false;
	        }

        }

        


    }
}
