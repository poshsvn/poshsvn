using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace PoshSvn.Tests.TestUtils
{
    public static class PSObjectAssert
    {
        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<PSObject> actual, params string[] excludeProperties)
        {
            var expectedEnumerator = expected.GetEnumerator();
            var actualEnumerator = actual.GetEnumerator();

            for (int index = 0; true; index++)
            {
                var expectedHasVal = expectedEnumerator.MoveNext();
                var actualHasVal = actualEnumerator.MoveNext();

                if (!expectedHasVal && !actualHasVal)
                {
                    break;
                }
                else if (!expectedHasVal && actualHasVal)
                {
                    Assert.Fail(string.Format("Extra actual value at index {0}:\r\n{1}",
                                index,
                                FormatObject(actualEnumerator.Current.BaseObject)));
                }
                else if (expectedHasVal && !actualHasVal)
                {
                    Assert.Fail(string.Format("Missing expected value at index {0}:\r\n{1}",
                                index,
                                FormatObject(expectedEnumerator.Current)));
                }

                T expectedObj = expectedEnumerator.Current;
                object actualObj = actualEnumerator.Current.BaseObject;

                ClassicAssert.AreEqual(expectedObj.GetType(), actualObj.GetType(), "Value types diffferent at index {0}", index);

                var type = expectedObj.GetType();

                bool isEqual = true;

                StringBuilder result = new StringBuilder();

                foreach (var propertyInfo in type.GetProperties())
                {
                    if (!excludeProperties.Contains(propertyInfo.Name))
                    {
                        object expectedVal = propertyInfo.GetValue(expectedObj);
                        object actualVal = propertyInfo.GetValue(actualObj);

                        if (Equals(expectedVal, actualVal))
                        {
                            result.AppendLine(string.Format("  {0,-24} = {1}", propertyInfo.Name, expectedVal));
                        }
                        else
                        {
                            isEqual = false;
                            result.AppendLine(string.Format("- {0,-24} = {1}", propertyInfo.Name, expectedVal));
                            result.AppendLine(string.Format("+ {0,-24} = {1}", propertyInfo.Name, actualVal));
                        }
                    }
                }

                if (!isEqual)
                {
                    Assert.Fail(string.Format("Values are different at index {0}\r\n{1}",
                                              index, result.ToString()));
                }
            }
        }

        static string FormatObject(object obj)
        {
            var result = new StringBuilder();

            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                object val = propertyInfo.GetValue(obj);
                result.AppendLine(string.Format("{0,-24} = {1}", propertyInfo.Name, val));
            }

            return result.ToString();
        }
    }
}
