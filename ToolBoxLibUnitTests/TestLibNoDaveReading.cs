﻿using DotNetSiemensPLCToolBoxLibrary.Communication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBoxLibUnitTests
{
    [TestClass]
    public class TestLibNoDaveReading
    {
        [TestMethod]
        public void TestReading()
        {
            var wrapper = new ConnectionWrapper(480);
            var conn = new PLCConnection(new PLCConnectionConfiguration(), wrapper);
            var listTag = new List<PLCTag>();
            listTag.Add(new PLCTag("P#DB60.DBX0 BYTE 300"));
            listTag.Add(new PLCTag("P#DB10.DBX10 BYTE 300"));
            conn.ReadValues(listTag, true);
            var pdus = wrapper.PDUs;
            string req = string.Join(Environment.NewLine, pdus);
            string t =
@"READ  Area:132, DBnum:10, Start:10, Bytes:300
READ  Area:132, DBnum:60, Start:0, Bytes:300";
            Assert.AreEqual(req, t);

            var wrapper2 = new ConnectionWrapper(480);
            var conn2 = new PLCConnection(new PLCConnectionConfiguration(), wrapper2);
            var listTag2 = new List<PLCTag>();
            listTag2.Add(new PLCTag("P#DB60.DBX0 BYTE 300"));
            listTag2.Add(new PLCTag("P#DB10.DBX10 BYTE 300"));
            conn2._TestNewReadValues(listTag2, true);
            var pdus2 = wrapper2.PDUs;
            string req2 = string.Join(Environment.NewLine, pdus2);
            string t2 = "READ  Area:132, DBnum:10, Start:10, Bytes:300\r\nREAD  Area:132, DBnum:60, Start:0, Bytes:300";
            Assert.AreEqual(req2, t2);

            var wrapper3 = new ConnectionWrapper(480);
            var conn3 = new PLCConnection(new PLCConnectionConfiguration(), wrapper3);
            var listTag3 = new List<PLCTag>();
            listTag3.Add(new PLCTag("P#DB60.DBX0 BYTE 300") { DontSplitValue = false });
            listTag3.Add(new PLCTag("P#DB10.DBX10 BYTE 300") { DontSplitValue = false });
            conn3.ReadValues(listTag3, true);
            var pdus3 = wrapper3.PDUs;
            string req3 = string.Join(Environment.NewLine, pdus3);
            string t3 = "READ  Area:132, DBnum:10, Start:10, Bytes:300\r\nREAD  Area:132, DBnum:60, Start:0, Bytes:140\r\nREAD  Area:132, DBnum:60, Start:140, Bytes:160";
            Assert.AreEqual(req3, t3);

            var wrapper4 = new ConnectionWrapper(480);
            var conn4 = new PLCConnection(new PLCConnectionConfiguration(), wrapper4);
            var listTag4 = new List<PLCTag>();
            listTag4.Add(new PLCTag("P#DB1.DBX0 BYTE 4") { DontSplitValue = false });
            listTag4.Add(new PLCTag("P#DB1.DBX8 BYTE 4") { DontSplitValue = false });
            listTag4.Add(new PLCTag("P#DB1.DBX10 BYTE 4") { DontSplitValue = false });
            listTag4.Add(new PLCTag("P#DB1.DBX30 BYTE 4") { DontSplitValue = false });
            listTag4.Add(new PLCTag("P#DB1.DBX50 BYTE 4") { DontSplitValue = false });
            listTag4.Add(new PLCTag("P#DB1.DBX80 BYTE 4") { DontSplitValue = false });
            conn4.ReadValues(listTag4, true);
            var pdus4 = wrapper4.PDUs;
            string req4 = string.Join(Environment.NewLine, pdus4);
            string t4 = "READ  Area:132, DBnum:1, Start:0, Bytes:14\r\nREAD  Area:132, DBnum:1, Start:30, Bytes:4\r\nREAD  Area:132, DBnum:1, Start:50, Bytes:4\r\nREAD  Area:132, DBnum:1, Start:80, Bytes:4";
            Assert.AreEqual(req4, t4);
        }
    }
}
