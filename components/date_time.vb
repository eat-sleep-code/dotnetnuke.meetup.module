Namespace DONEIN_NET.Meetup

Public Class Date_Time

		'// ==============================================================================================================================================
		'//
		'// Returns Popular Formats Of Short Date & Time Strings
		'//  
		'// The value of vbShortDate is based upon the server's Regional Settings.  If you do not have administrative rights on the web server you may 
		'//	not have access to Regional Settings on your server.  This function makes an attempt to overcome this problem.  It only supports common short
		'// date strings and may not fit your needs.
		'//	
		'// Usage: date_time.format(Now(), "mm/dd/yyyy")
		'//
		'// ==============================================================================================================================================

		Public Function format(date_time As DateTime, Optional return_format As String = "") As String
			Dim tmp_return_format As String = return_format.ToLower.Trim
			Dim tmp_date_format As String
			Dim tmp_time_format As String


			'// SPLIT APART THE DATE
			Dim tmp_month As String = DatePart(DateInterval.Month,date_time,Microsoft.VisualBasic.FirstDayOfWeek.Sunday,FirstWeekOfYear.Jan1).ToString("0#")
			Dim tmp_day As String = DatePart(DateInterval.Day,date_time,Microsoft.VisualBasic.FirstDayOfWeek.Sunday,FirstWeekOfYear.Jan1).ToString("0#")
			Dim tmp_year As String = Right("20" + DatePart(DateInterval.Year,date_time,Microsoft.VisualBasic.FirstDayOfWeek.Sunday,FirstWeekOfYear.Jan1).ToString, 4)
			

			'// SPLIT APART THE TIME
			Dim tmp_hour As String = DatePart(DateInterval.Hour,date_time,Microsoft.VisualBasic.FirstDayOfWeek.Sunday,FirstWeekOfYear.Jan1).ToString("0#")
			Dim tmp_minute As String = DatePart(DateInterval.Minute,date_time,Microsoft.VisualBasic.FirstDayOfWeek.Sunday,FirstWeekOfYear.Jan1).ToString("0#")
			Dim tmp_second As String = DatePart(DateInterval.Second,date_time,Microsoft.VisualBasic.FirstDayOfWeek.Sunday,FirstWeekOfYear.Jan1).ToString("0#")
			Dim tmp_ampm As String
			
			
			

			'// FORMAT THE DATE
				If tmp_return_format.StartsWith("mm/dd/yyyy") Then
					tmp_date_format = tmp_month + "/" + tmp_day + "/" + tmp_year + " "	
				ElseIf tmp_return_format.StartsWith("dd/mm/yyyy") Then
					tmp_date_format = tmp_day + "/" + tmp_month + "/" + tmp_year + " "
				ElseIf tmp_return_format.StartsWith("yyyy/mm/dd") Then
					tmp_date_format = tmp_year + "/" + tmp_month + "/" + tmp_day + " "
				ElseIf tmp_return_format.StartsWith("yyyy-mm-dd") Then
					tmp_date_format = tmp_year + "-" + tmp_month + "-" + tmp_day + " "
				ElseIf tmp_return_format.StartsWith("yyyymmdd") Then
					tmp_date_format = tmp_year + tmp_month + tmp_day
				Else
					tmp_date_format = ""
				End If

			


			'// FORMAT THE TIME
				
			If tmp_return_format.EndsWith("hhmmss") Then
				tmp_time_format = tmp_hour + tmp_minute + tmp_second
			ElseIf tmp_return_format.EndsWith("hh:mm:ss") Then
				tmp_time_format = tmp_hour + ":" + tmp_minute + ":" + tmp_second	
			ElseIf tmp_return_format.EndsWith("hh:mm") Then
				tmp_time_format = tmp_hour + ":" + tmp_minute
			ElseIf tmp_return_format.EndsWith("hh:mm:ss ampm") Then
				If CType(tmp_hour,Integer) > 12 Then
					tmp_hour = (CType(tmp_hour,Integer) - 12 ).ToString
					tmp_ampm = "PM"
				Else
					IF CType(tmp_hour,Integer) = 0 Then
						tmp_hour = "12"
					Else
						tmp_hour = tmp_hour.ToString
					End If
					tmp_ampm = "AM"
				End If
				tmp_time_format = tmp_hour + ":" + tmp_minute + ":" + tmp_second + " " + tmp_ampm
			ElseIf tmp_return_format.EndsWith("hh:mm ampm") Then
				If CType(tmp_hour,Integer) > 12 Then
					tmp_hour = (CType(tmp_hour,Integer) - 12 ).ToString
					tmp_ampm = "PM"
				Else
					tmp_hour = tmp_hour.ToString
					tmp_ampm = "AM"
				End If
				tmp_time_format = tmp_hour + ":" + tmp_minute + " " + tmp_ampm
			End If
	  
			Return (tmp_date_format + tmp_time_format).Trim

		End Function


		Public Function timestamp(Optional unique_suffix As String = "000000") As String
			'// RETURNS UNIQUE 24 DIGIT TIMESTAMP
			Dim seed As DateTime = DateTime.Now
			Return Left(Right("0000" + seed.Year.ToString, 4) + seed.Month.ToString("0#") + seed.Day.ToString("0#") + seed.Hour.ToString("0#") + seed.Minute.ToString("0#") + seed.Second.ToString("0#") + Left(seed.Millisecond.ToString + "0000", 4) + Right("00000000" + unique_suffix, 6) + "00000000000000000000000000",24)
		End Function
	    
	End Class

End Namespace
