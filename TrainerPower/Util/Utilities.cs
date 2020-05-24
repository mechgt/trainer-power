// <copyright file="Utilities.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace TrainerPower.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Data;

    /// <summary>
    /// Generic utilities class that can be used on many projects
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        internal static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return constructedString;
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        internal static byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Remove paused time from data tracks
        /// </summary>
        /// <param name="sourceTrack"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static INumericTimeDataSeries RemovePausedTimesInTrack(INumericTimeDataSeries sourceTrack, IActivity activity)
        {
            ActivityInfo activityInfo = ActivityInfoCache.Instance.GetInfo(activity);

            if (activityInfo != null && sourceTrack != null)
            {
                INumericTimeDataSeries result = new NumericTimeDataSeries();

                if (activityInfo.NonMovingTimes.Count == 0)
                {
                    // Remove invalid data nonetheless
                    DateTime currentTime = sourceTrack.StartTime;
                    IEnumerator<ITimeValueEntry<float>> sourceEnumerator = sourceTrack.GetEnumerator();
                    bool sourceEnumeratorIsValid;

                    sourceEnumeratorIsValid = sourceEnumerator.MoveNext();

                    while (sourceEnumeratorIsValid)
                    {
                        if (!float.IsNaN(sourceEnumerator.Current.Value))
                        {
                            result.Add(currentTime, sourceEnumerator.Current.Value);
                        }

                        sourceEnumeratorIsValid = sourceEnumerator.MoveNext();
                        currentTime = sourceTrack.StartTime + new TimeSpan(0, 0, (int)sourceEnumerator.Current.ElapsedSeconds);
                    }
                }
                else
                {
                    DateTime currentTime = sourceTrack.StartTime;
                    IEnumerator<ITimeValueEntry<float>> sourceEnumerator = sourceTrack.GetEnumerator();
                    IEnumerator<IValueRange<DateTime>> pauseEnumerator = activityInfo.NonMovingTimes.GetEnumerator();
                    double totalPausedTimeToDate = 0;
                    bool sourceEnumeratorIsValid;
                    bool pauseEnumeratorIsValid;

                    pauseEnumeratorIsValid = pauseEnumerator.MoveNext();
                    sourceEnumeratorIsValid = sourceEnumerator.MoveNext();

                    while (sourceEnumeratorIsValid)
                    {
                        bool addCurrentSourceEntry = true;
                        bool advanceCurrentSourceEntry = true;

                        // Loop to handle all pauses up to this current track point
                        if (pauseEnumeratorIsValid)
                        {
                            if (currentTime > pauseEnumerator.Current.Lower &&
                                currentTime <= pauseEnumerator.Current.Upper)
                            {
                                addCurrentSourceEntry = false;
                            }
                            else if (currentTime > pauseEnumerator.Current.Upper)
                            {
                                // Advance pause enumerator
                                totalPausedTimeToDate += (pauseEnumerator.Current.Upper - pauseEnumerator.Current.Lower).TotalSeconds;
                                pauseEnumeratorIsValid = pauseEnumerator.MoveNext();

                                // Make sure we retry with the next pause
                                addCurrentSourceEntry = false;
                                advanceCurrentSourceEntry = false;
                            }
                        }

                        if (addCurrentSourceEntry && !float.IsNaN(sourceEnumerator.Current.Value))
                        {
                            DateTime entryTime = currentTime - new TimeSpan(0, 0, (int)totalPausedTimeToDate);

                            result.Add(entryTime, sourceEnumerator.Current.Value);
                        }

                        if (advanceCurrentSourceEntry)
                        {
                            sourceEnumeratorIsValid = sourceEnumerator.MoveNext();
                            currentTime = sourceTrack.StartTime + new TimeSpan(0, 0, (int)sourceEnumerator.Current.ElapsedSeconds);
                        }
                    }
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// Perform a smoothing operation using a moving average on the data series
        /// </summary>
        /// <param name="track">The data series to smooth</param>
        /// <param name="period">The range to smooth.  This is the total number of seconds to smooth across (slightly different than the ST method.)</param>
        /// <param name="min">An out parameter set to the minimum value of the smoothed data series</param>
        /// <param name="max">An out parameter set to the maximum value of the smoothed data series</param>
        /// <returns></returns>
        internal static INumericTimeDataSeries Smooth(INumericTimeDataSeries track, uint period, out float min, out float max)
        {
            min = float.NaN;
            max = float.NaN;
            INumericTimeDataSeries smooth = new NumericTimeDataSeries();

            if (track != null && track.Count > 0 && period > 1)
            {
                //min = float.NaN;
                //max = float.NaN;
                int start = 0;
                int index = 0;
                float value = 0;
                float delta;

                float per = period;

                // Iterate through track
                // For each point, create average starting with 'start' index and go forward averaging 'period' seconds.
                // Stop when last 'full' period can be created ([start].ElapsedSeconds + 'period' seconds >= TotalElapsedSeconds)
                while (track[start].ElapsedSeconds + period < track.TotalElapsedSeconds)
                {
                    while (track[index].ElapsedSeconds < track[start].ElapsedSeconds + period)
                    {
                        delta = track[index + 1].ElapsedSeconds - track[index].ElapsedSeconds;
                        value += track[index].Value * delta;
                        index++;
                    }

                    // Finish value calculation
                    per = track[index].ElapsedSeconds - track[start].ElapsedSeconds;
                    value = value / per;

                    // Add value to track
                    // I really don't need the smoothed track... really just need max.  Kill this for efficiency?
                    smooth.Add(track.EntryDateTime(track[index]), value);

                    // Remove beginning point for next cycle
                    delta = track[start + 1].ElapsedSeconds - track[start].ElapsedSeconds;
                    value = (per * value - delta * track[start].Value);

                    // Next point
                    start++;
                }

                max = smooth.Max;
                min = smooth.Min;
            }
            else if (track != null && track.Count > 0 && period == 1)
            {
                min = track.Min;
                max = track.Max;
                return track;
            }

            return smooth;
        }

        internal static INumericTimeDataSeries STSmooth(INumericTimeDataSeries data, int seconds, out float min, out float max)
        {
            min = float.NaN;
            max = float.NaN;
            if (data.Count == 0)
            {
                // Special case, no data
                return new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
            }
            else if (data.Count == 1 || seconds < 1)
            {
                // Special case
                INumericTimeDataSeries copyData = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
                min = data[0].Value;
                max = data[0].Value;
                foreach (ITimeValueEntry<float> entry in data)
                {
                    copyData.Add(data.StartTime.AddSeconds(entry.ElapsedSeconds), entry.Value);
                    min = Math.Min(min, entry.Value);
                    max = Math.Max(max, entry.Value);
                }
                return copyData;
            }
            min = float.MaxValue;
            max = float.MinValue;
            int smoothWidth = Math.Max(0, seconds * 2); // Total width/period.  'seconds' is the half-width... seconds on each side to smooth
            int denom = smoothWidth * 2; // Final value to divide by.  It's divide by 2 because we're double-adding everything
            INumericTimeDataSeries smoothedData = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();

            // Loop through entire dataset
            for (int nEntry = 0; nEntry < data.Count; nEntry++)
            {
                ITimeValueEntry<float> entry = data[nEntry];
                
                double value = 0;
                double delta;
                // Data prior to entry
                long secondsRemaining = seconds;
                ITimeValueEntry<float> p1, p2;
                int increment = -1;
                int pos = nEntry - 1;
                p2 = data[nEntry];


                while (secondsRemaining > 0 && pos >= 0)
                {
                    p1 = data[pos];
                    if (SumValues(p2, p1, ref value, ref secondsRemaining))
                    {
                        pos += increment;
                        p2 = p1;
                    }
                    else
                    {
                        break;
                    }
                }
                if (secondsRemaining > 0)
                {
                    // Occurs at beginning of track when period extends before beginning of track.
                    delta = data[0].Value * secondsRemaining * 2;
                    value += delta;
                }
                // Data after entry
                secondsRemaining = seconds;
                increment = 1;
                pos = nEntry;
                p1 = data[nEntry];
                while (secondsRemaining > 0 && pos < data.Count - 1)
                {
                    p2 = data[pos + 1];
                    if (SumValues(p1, p2, ref value, ref secondsRemaining))
                    {
                        // Move to next point
                        pos += increment;
                        p1 = p2;
                    }
                    else
                    {
                        break;
                    }
                }
                if (secondsRemaining > 0)
                {
                    // Occurs at end of track when period extends past end of track
                    value += data[data.Count - 1].Value * secondsRemaining * 2;
                }
                float entryValue = (float)(value / denom);
                smoothedData.Add(data.StartTime.AddSeconds(entry.ElapsedSeconds), entryValue);
                min = Math.Min(min, entryValue);
                max = Math.Max(max, entryValue);

                // STSmooth: TODO: Remove 'first' p1 & p2 SumValues from 'value'
                if (data[nEntry].ElapsedSeconds - seconds < 0)
                {
                    // Remove 1 second worth of first data point (multiply by 2 because everything is double here)
                    value -= data[0].Value * 2;
                }
                else
                {
                    // Remove data in middle of track (typical scenario)
                    //value -= 
                }
            }
            return smoothedData;
        }

        private static bool SumValues(ITimeValueEntry<float> p1, ITimeValueEntry<float> p2, ref double value, ref long secondsRemaining)
        {
            double spanSeconds = Math.Abs((double)p2.ElapsedSeconds - (double)p1.ElapsedSeconds);
            if (spanSeconds <= secondsRemaining)
            {
                value += (p1.Value + p2.Value) * spanSeconds;
                secondsRemaining -= (long)spanSeconds;
                return true;
            }
            else
            {
                double percent = (double)secondsRemaining / (double)spanSeconds;
                value += (p1.Value * ((float)2 - percent) + p2.Value * percent) * secondsRemaining;
                secondsRemaining = 0;
                return false;
            }
        }
    }
}
