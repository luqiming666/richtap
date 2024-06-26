﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trivial.Text;

namespace RichTap;

/// <summary>
/// The vibration motor.
/// </summary>
public static class VibrationMotor
{
    private static bool init;
    private static readonly object obj = new();

    static VibrationMotor()
    {
        try
        {
            VibrationMotorWrapper.Init();
            init = true;
            VibrationMotorWrapper.RegisterCallback(OnGameControllerChange);
        }
        catch (TypeInitializationException)
        {
        }
        catch (TypeLoadException)
        {
        }
        catch (InvalidOperationException)
        {
        }
        catch (NotSupportedException)
        {
        }
        catch (ExternalException)
        {
        }
    }

    /// <summary>
    /// Adds or removes the event handler on a game controller is attached or disattached.
    /// </summary>
    public static event EventHandler<EventArgs> GameControllerChanged;

    /// <summary>
    /// Gets the initialization state of the SDK.
    /// </summary>
    public static bool GetInitState => init;

    /// <summary>
    /// Sets initialization state of the SDK.
    /// </summary>
    /// <param name="initialize">true if initializes; otherwise, false.</param>
    /// <exception cref="InvalidOperationException">Initialize or release failed.</exception>
    public static void SetInitState(bool initialize)
    {
        if (initialize == init) return;
        try
        {
            if (initialize) VibrationMotorWrapper.Init();
            else VibrationMotorWrapper.Destroy();
            init = initialize;
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException(initialize ? "Initialize failed." : "Destroy failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException(initialize ? "Initialize failed." : "Destroy failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException(initialize ? "Initialize failed." : "Destroy failed.", ex);
        }
    }

    /// <summary>
    /// Gets the internal SDK version of RichTap.
    /// </summary>
    /// <returns>The version; or null, if not supported.</returns>
    public static string Version()
    {
        try
        {
            return VibrationMotorWrapper.GetVersion();
        }
        catch (TypeInitializationException)
        {
        }
        catch (TypeLoadException)
        {
        }
        catch (InvalidOperationException)
        {
        }
        catch (NotSupportedException)
        {
        }
        catch (ExternalException)
        {
        }

        return null;
    }

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public static void Play(string he, VibrationOptions options = null)
    {
        if (string.IsNullOrEmpty(he)) return;
        options ??= new();
        try
        {
            VibrationMotorWrapper.Play(he, options.LoopCount, options.LoopIntervalMilliseconds, options.GainIntValue, options.FrequencyFactorIntValue);
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
    }

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="start">The start position.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public static void Play(string he, TimeSpan start, VibrationOptions options = null)
    {
        if (string.IsNullOrEmpty(he)) return;
        options ??= new();
        try
        {
            VibrationMotorWrapper.PlaySection(he, options.LoopCount, options.LoopIntervalMilliseconds, options.GainIntValue, options.FrequencyFactorIntValue, GetMilliseconds(start), int.MaxValue);
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
    }

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public static void Play(string he, TimeSpan start, TimeSpan end, VibrationOptions options = null)
    {
        if (string.IsNullOrEmpty(he)) return;
        options ??= new();
        if (end <= start) return;
        try
        {
            VibrationMotorWrapper.PlaySection(he, options.LoopCount, options.LoopIntervalMilliseconds, options.GainIntValue, options.FrequencyFactorIntValue, GetMilliseconds(start), GetMilliseconds(end));
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException("Play failed.", ex);
        }
    }

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public static void Play(JsonObjectNode he, VibrationOptions options = null)
        => Play(he?.ToString(), options);

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="start">The start position.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public static void Play(JsonObjectNode he, TimeSpan start, VibrationOptions options = null)
        => Play(he?.ToString(), start, options);

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public static void Play(JsonObjectNode he, TimeSpan start, TimeSpan end, VibrationOptions options = null)
        => Play(he?.ToString(), start, end, options);

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    /// <exception cref="DllNotFoundException">The implementation assembly required does not exist.</exception>
    public static void Play(VibrationDescriptionModel he, VibrationOptions options = null)
        => Play(he?.ToString(), options);

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="start">The start position.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public static void Play(VibrationDescriptionModel he, TimeSpan start, VibrationOptions options = null)
        => Play(he?.ToString(), start, options);

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public static void Play(VibrationDescriptionModel he, TimeSpan start, TimeSpan end, VibrationOptions options = null)
        => Play(he?.ToString(), start, end, options);

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="ArgumentNullException">he is null.</exception>
    /// <exception cref="IOException">Read file failed.</exception>
    /// <exception cref="UnauthorizedAccessException">Unauthorized to access the file.</exception>
    /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
    /// <exception cref="NotSupportedException">The path of filee is in an invalid format or not supported to read.</exception>
    public static void Play(FileInfo he, VibrationOptions options = null)
        => Play(GetFileString(he), options);

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="start">The start position.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="ArgumentNullException">he is null.</exception>
    /// <exception cref="IOException">Read file failed.</exception>
    /// <exception cref="UnauthorizedAccessException">Unauthorized to access the file.</exception>
    /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
    /// <exception cref="NotSupportedException">The path of filee is in an invalid format or not supported to read.</exception>
    public static void Play(FileInfo he, TimeSpan start, VibrationOptions options = null)
        => Play(GetFileString(he), start, options);

    /// <summary>
    /// Plays a specific HE format content.
    /// </summary>
    /// <param name="he">The HE format content.</param>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="InvalidOperationException">Play failed.</exception>
    /// <exception cref="ArgumentNullException">he is null.</exception>
    /// <exception cref="IOException">Read file failed.</exception>
    /// <exception cref="UnauthorizedAccessException">Unauthorized to access the file.</exception>
    /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
    /// <exception cref="NotSupportedException">The path of filee is in an invalid format or not supported to read.</exception>
    public static void Play(FileInfo he, TimeSpan start, TimeSpan end, VibrationOptions options = null)
        => Play(GetFileString(he), start, end, options);

    /// <summary>
    /// Stops current playback.
    /// </summary>
    /// <exception cref="InvalidOperationException">Stop failed.</exception>
    public static void Stop()
    {
        try
        {
            VibrationMotorWrapper.Stop();
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException("Stop failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException("Stop failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException("Stop failed.", ex);
        }
    }

    /// <summary>
    /// Updates loop parameters.
    /// </summary>
    /// <param name="interval">The loop interval.</param>
    /// <param name="gain">The vibration gain.</param>
    /// <param name="frequencyFactor">The factor of frequency.</param>
    /// <exception cref="InvalidOperationException">Action is failed.</exception>
    public static void UpdateLoopParameters(TimeSpan interval, double gain, double frequencyFactor)
    {
        try
        {
            VibrationMotorWrapper.SendLoopParam(GetMilliseconds(interval), VibrationOptions.GetGainValue(gain), VibrationOptions.GetFrequencyFactorValue(frequencyFactor));
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException("Update loop parameters failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException("Update loop parameters failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException("Update loop parameters failed.", ex);
        }
    }

    /// <summary>
    /// Gets all game controllers.
    /// </summary>
    /// <returns>The name list of game controller.</returns>
    /// <exception cref="InvalidOperationException">Action is failed.</exception>
    public static List<string> ListGameControllers()
    {
        try
        {
            var s = VibrationMotorWrapper.GameControllers();
            if (string.IsNullOrEmpty(s)) return new();
            var json = JsonObjectNode.TryParse(s);
            return json?.TryGetStringListValue("controllers", true) ?? new();
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException("List connected game controllers failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException("List connected game controllers failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException("List connected game controllers failed.", ex);
        }
    }

    /// <summary>
    /// Sets a value indicating whether enable signal converter.
    /// </summary>
    /// <param name="enable">true if enable signal converter; otherwise, false.</param>
    /// <exception cref="InvalidOperationException">Action is failed.</exception>
    public static bool SignalConverterState(bool enable)
    {
        try
        {
            return VibrationMotorWrapper.SignalConverterState(enable);
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException(enable ? "Enable signal converter failed." : "Disable signal converter failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException(enable ? "Enable signal converter failed." : "Disable signal converter failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException(enable ? "Enable signal converter failed." : "Disable signal converter failed.", ex);
        }
    }

    /// <summary>
    /// Sets a value indicating whether enable rumble.
    /// </summary>
    /// <param name="enable">true if enable rumble; otherwise, false.</param>
    /// <exception cref="InvalidOperationException">Action is failed.</exception>
    public static bool RumbleState(bool enable)
    {
        try
        {
            return VibrationMotorWrapper.RumbleState(enable);
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException(enable ? "Enable rumble failed." : "Disable rumble failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException(enable ? "Enable rumble failed." : "Disable rumble failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException(enable ? "Enable rumble failed." : "Disable rumble failed.", ex);
        }
    }

    /// <summary>
    /// Sets a value indicating whether enable debug log.
    /// </summary>
    /// <param name="enable">true if enable debug log; otherwise, false.</param>
    /// <exception cref="InvalidOperationException">Action is failed.</exception>
    public static void SetDebugLog(bool enable)
    {
        try
        {
            VibrationMotorWrapper.DebugLog(enable);
        }
        catch (TypeInitializationException ex)
        {
            throw new InvalidOperationException(enable ? "Enable debug log output failed." : "Disable debug log output failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new InvalidOperationException(enable ? "Enable debug log output failed." : "Disable debug log output failed.", ex);
        }
        catch (ExternalException ex)
        {
            throw new InvalidOperationException(enable ? "Enable debug log output failed." : "Disable debug log output failed.", ex);
        }
    }

    internal static int GetMilliseconds(TimeSpan span)
    {
        var ms = span.TotalMilliseconds;
        if (ms <= 0) return 0;
        if (ms > int.MaxValue) return int.MaxValue;
        return (int)ms;
    }

    private static void OnGameControllerChange(string data, int size)
        => GameControllerChanged?.Invoke(obj, new EventArgs());

    private static string GetFileString(FileInfo file)
        => file == null ? throw new ArgumentNullException(nameof(file)) : File.ReadAllText(file.FullName);
}
