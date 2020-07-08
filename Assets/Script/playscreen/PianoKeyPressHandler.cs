using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Melanchall.DryWetMidi.Core;
using UnityEngine;
using Melanchall.DryWetMidi.Devices;
using Script.playscreen;
using Script.playscreen.Observer;

public class PianoKeyPressHandler : MonoBehaviour{
    private InputDevice _inputDevice;
    private List<NoteOnEvent> _inputs = new List<NoteOnEvent>();
    private bool _waiting;
    private EventWaitHandle _eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
    private bool _update;

    // Start is called before the first frame _update
    void Start() {
        if ((InputDevice.GetAll() is null)) {
            return;
        }
        _inputDevice = InputDevice.GetById(0);
        _inputDevice.EventReceived += OnEventReceived;
        _inputDevice.StartEventsListening();
    }

    private void OnEventReceived(object sender, MidiEventReceivedEventArgs e) {
        // skip any events that are not key-down presses.
        if (!(e.Event.EventType.Equals(MidiEventType.NoteOn))) {
            return;
        }

        _inputs.Add((NoteOnEvent)e.Event);
        if (!_waiting) {
            // catch all _inputs within 0.1 seconds, for chords.
            Thread thread = new Thread(() => {
                _waiting = true;
                Thread.Sleep(100);
                _update = true;
                
            });
            thread.Start();
        }
    }
    
    private IEnumerator WaitThenNotify() {
        yield return new WaitForSeconds(0.1f);

        PianoPressObserver.UpdateSubjects(_inputs);
        _waiting = false;
        _inputs.Clear();
    }

    // Update is called once per frame
    private void Update() {
        if (_update) {
            _update = false;
            PianoPressObserver.UpdateSubjects(_inputs);
            _waiting = false;
            _inputs.Clear();
        }
    }

    private void OnDestroy() {
        // dispose/free the _inputDevice.
        (_inputDevice as IDisposable)?.Dispose();
    }
}