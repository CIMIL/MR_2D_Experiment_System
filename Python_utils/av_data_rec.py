from __future__ import print_function, division
import numpy as np
import pyaudio
import wave
import threading
import time
import sys
import pyzed.sl as sl
from signal import signal, SIGINT
import tkinter as tk
from tkinter import *
from tkinter.messagebox import showerror
from datetime import date, datetime
import argparse
from pythonosc import udp_client
from pythonosc.dispatcher import Dispatcher
from pythonosc import osc_server
from pygame import mixer

soundcardInputId = 9

path = "" #path to save output files

#Global
frames_recorded = 0
nomefileglobale= ""
nomefilewav= ""
numcanaleoutput= 121

today = date.today()
d = today.strftime("%b-%d-%Y")

tim = time.localtime()
current_time = time.strftime("%H-%M-%S", tim)

dt = datetime.now()
startt = datetime.timestamp(dt)

# ZED CAMERA RECORDER
class VideoRecorder():  
    nome=""
    def prendinome(self,n):
        return n
    
    def __init__(self, ):
        nome=self.prendinome(nomefileglobale)
        self.open = True
        self.camera = sl.Camera()
        self.camera_resolution = sl.RESOLUTION.HD720
        self.depth_mode = sl.DEPTH_MODE.ULTRA #
        self.coordinate_units = sl.UNIT.METER
        self.camera_fps = 60
        self.coordinate_system = sl.COORDINATE_SYSTEM.RIGHT_HANDED_Y_UP
        self.init = sl.InitParameters(camera_resolution=self.camera_resolution,
                                    depth_mode=self.depth_mode,
                                    coordinate_units=self.coordinate_units,
                                    depth_minimum_distance = 1.0, #
                                    depth_maximum_distance=3.0, #
                                    camera_fps = self.camera_fps,
                                    coordinate_system=self.coordinate_system,
                                    depth_stabilization=0) #
        self.path_output = path + nome + d + current_time + ".svo"
        self.recording_param = sl.RecordingParameters(self.path_output, sl.SVO_COMPRESSION_MODE.H264)
        self.runtime = sl.RuntimeParameters(confidence_threshold = 20, 	texture_confidence_threshold = 100) 
        status = self.camera.open(self.init)
        err = self.camera.enable_recording(self.recording_param)
        if err != sl.ERROR_CODE.SUCCESS:
            print(repr(status))
            exit(1)


    def record(self):

        global frames_recorded
        while self.open:
            if self.camera.grab(self.runtime) == sl.ERROR_CODE.SUCCESS :
                frames_recorded += 1
                print("Frame count: " + str(frames_recorded), end="\r")
            else:
                break
   
    def stop(self):
        "Finishes the video recording therefore the thread too"
        if self.open:            
            self.open=False

    def start(self):
        dt = datetime.now()
        # getting the timestamp
        ts = datetime.timestamp(dt)
        print("---> video: " + str(ts - startt))
        "Launches the video recording function using a thread"
        video_thread = threading.Thread(target=self.record)
        #global gui_thread
        dt1 = datetime.now()
        ts1 = datetime.timestamp(dt1)
        print("---> video thread 1: " + str(ts1 - startt))
        video_thread.start()
        print(self.path_output)
        
# AUDIO RECORDER
class AudioRecorder():
    nome=""
    numcanale=""
    def prendinome(self,n):
        return n

    def prendinumero(self,num):
        return num

    # https://people.csail.mit.edu/hubert/pyaudio/docs/
    # pyaudio.paALSA

    # 1024
    def __init__(self, rate=44100, chunk=32, channels=1):
        nome=self.prendinome(nomefileglobale)
        numcanale=self.prendinumero(soundcardInputId)
        self.open = True
        self.rate = rate
        self.frames_per_buffer = chunk
        self.channels = channels
        self.format = pyaudio.paMME 
        self.audio_filename = "C:/Users/user/Desktop/Alberto/Develop/Lab_PC/Zed_project/Python-sdk/av-record/out/tracks/" + nomefileglobale + d +  current_time + ".wav"
        self.audio = pyaudio.PyAudio()
        self.stream = self.audio.open(format=self.format,
                                      channels=self.channels,
                                      rate=self.rate,
                                      input=True,
                                      input_device_index= int(numcanale),
                                      frames_per_buffer = self.frames_per_buffer)
        self.audio_frames = []
        self.stream.start_stream()

    def record(self):
        "Audio starts being recorded"
        while self.open:
            data = self.stream.read(self.frames_per_buffer, exception_on_overflow=False) 
            self.audio_frames.append(data)
            if not self.open:
                break

    def stop(self):
        "Finishes the audio recording therefore the thread too"
        if self.open:
            self.open = False
            self.stream.stop_stream()
            self.stream.close()
            self.audio.terminate()
            waveFile = wave.open(self.audio_filename, 'wb')
            waveFile.setnchannels(self.channels)
            waveFile.setsampwidth(self.audio.get_sample_size(self.format))
            waveFile.setframerate(self.rate)
            waveFile.writeframes(b''.join(self.audio_frames))
            waveFile.close()

    

    def start(self):
        dt = datetime.now()
        # getting the timestamp
        ts = datetime.timestamp(dt)
        print("---> audio: " + str(ts - startt))
        "Launches the audio recording function using a thread"
        audio_thread = threading.Thread(target=self.record)
        #global gui_thread
        dt1 = datetime.now()
        ts1 = datetime.timestamp(dt1)
        print("---> audio thread 1: " + str(ts1 - startt))
        audio_thread.start()

def start_AVrecording(filename="test"):
    #global gui_thread
    dt = datetime.now()
    # getting the timestamp
    ts = datetime.timestamp(dt)
    print("---> thread: " + str(ts - startt))
    global audio_thread
    global video_thread

    sendMess("t/1/record")
    
    audio_thread.start()
    video_thread.start()
    
    dt1 = datetime.now()
    ts1 = datetime.timestamp(dt1)
    print("---> thread 1: " + str(ts1 - startt))
    return filename

def stop_AVrecording(filename="test"):
    
    audio_thread.stop() 
    
    video_thread.stop() 
    sendMess("t/1/stop")

    # Makes sure the threads have finished
    while threading.active_count() > 1:
        time.sleep(1)
 
def invianomefile(nomefilepers):
    global nomefileglobale
    nomefileglobale=nomefilepers

def invianumerocanale(numcanale):
    global soundcardInputId
    soundcardInputId=numcanale

def StartGrab():   
    start_AVrecording()

def StopGrab():
    stop_AVrecording()

def aggiornaFrame(frame_lbl):
    print(frames_recorded)
    frame_lbl.config(text=frames_recorded)

def invianomefilerec(nfile):
    global nomefilewav
    nomefilewav=nfile

def inviacanaleoutput(numcan):
    global numcanaleoutput
    numcanaleoutput=numcan

def sendMess(address):
    parser = argparse.ArgumentParser()
    parser.add_argument("--ip", default="192.168.163.226")
    parser.add_argument("--port", type=int, default=8000)
    args = parser.parse_args()

    client = udp_client.SimpleUDPClient(args.ip, args.port)

    client.send_message(address,int(1))

def inizializzazione():
    global audio_thread
    global video_thread

    audio_thread = AudioRecorder()
    video_thread = VideoRecorder()
   
def control_gui():
    gui = tk.Tk()

    nomefilepers=tk.StringVar() #memorizzo nomefile da registrare
    numcanale=tk.StringVar() #memorizzo numero canale input
    nomefilewav=tk.StringVar() #memorizzo nomefile registrato
    numcanaleoutput=tk.StringVar() #memorizzo numero canale output

    gui.geometry("1200x500")
    gui.title("Centro comandi")
    gui.configure(background='#669999')

    #pulsante inizio registrazione
    start_button = tk.Button(gui, 
                  text ="REC", 
                  width=15, 
                  height=5, 
                  bg="white", 
                  fg="black", 
                  command = StartGrab).grid(row=3, column=2)
    
    #pulsante stop registrazione
    stop_button = tk.Button(gui, 
                  text ="STOP", 
                  width=15, 
                  height=5, 
                  bg="#ff3333", 
                  fg="black", 
                  command = StopGrab).grid(row=3, column=4, pady=20)

    #label per il numero di frame registrati
    frame_lbl = tk.Label(
                    gui,
                    bg='#0f4b6e',
                    fg='white', text=0)
    frame_lbl.grid(row=4, column=4)

    #entry per il nome del file da registrare
    entry= Entry(gui, textvariable=nomefilepers, width= 40).grid(row=0, column=1)

    #label per il nome del file da registrare
    nomefile = tk.Label(
                    gui,
                    text="Inserire nome file",
                    bg='#0f4b6e',
                    fg='white').grid(row=0, column=0,padx=10,pady=10)
    
    #pulsante per la convalida del nome del file da registrare
    invianome = tk.Button(gui, 
                  text ="Convalida nome file", 
                  width=15, 
                  height=5, 
                  bg="#66ff99", 
                  fg="black", 
                  command = lambda : invianomefile(nomefilepers.get())).grid(row=0, column=2, padx=20,pady=20)
    
    #entry per il nome del file wav da riprodurre
    entry= Entry(gui, textvariable=nomefilewav, width= 40).grid(row=0, column=5)

    #label per il nome del file wav da riprodurre
    nomefile = tk.Label(
                    gui,
                    text="Inserire nome file wav da riprodurre",
                    bg='#0f4b6e',
                    fg='white').grid(row=0, column=4,padx=10,pady=10)
    
    #pulsante per la convalida del nome del file wav da riprodurre
    invianome = tk.Button(gui, 
                  text ="Convalida nome file", 
                  width=15, 
                  height=5, 
                  bg="#ff00ff", 
                  fg="black", 
                  command = lambda : invianomefilerec(nomefilewav.get())).grid(row=0, column=6, padx=20,pady=20)

    
    #entry per il numero del canale input
    entrycanale= Entry(gui, textvariable=numcanale, width= 40).grid(row=1, column=1)

    #label per il numero del canale input
    inputnumber = tk.Label(
                    gui,
                    text="Inserire numero canale di input",
                    bg='#0f4b6e',
                    fg='white').grid(row=1, column=0, padx=15,pady=15)
    
    #pulsante per la convalida del numero del canale input
    invianumero = tk.Button(gui, 
                  text ="Convalida numero \n canale input", 
                  width=15, 
                  height=5, 
                  bg="#ff9900", 
                  fg="black", 
                  command = lambda : invianumerocanale(numcanale.get())).grid(row=1, column=2)
    
    #entry per il numero del canale di output
    entrycanale= Entry(gui, textvariable=numcanaleoutput, width= 40).grid(row=1, column=5)

    #label per il numero del canale di output
    inputnumber = tk.Label(
                    gui,
                    text="Inserire numero canale di output",
                    bg='#0f4b6e',
                    fg='white').grid(row=1, column=4, padx=15,pady=15)
    
    #pulsante per la convalida del numero del canale
    invianumero = tk.Button(gui, 
                  text ="Convalida numero \n canale output", 
                  width=15, 
                  height=5, 
                  bg="#ff9900", 
                  fg="black", 
                  command = lambda : inviacanaleoutput(numcanaleoutput.get())).grid(row=1, column=6)

    
    #pulsante per la visualizzazione del numero di frame registrati
    aggiorna = tk.Button(gui, 
                  text ="Visualizza numero \n frame registrati", 
                  width=15, 
                  height=5, 
                  bg="#9933ff", 
                  fg="white", 
                  command = lambda: aggiornaFrame(frame_lbl)).grid(row=4, column=2, pady=20)
    
    #pulsante per preparare i thread audio e video
    preparaThread = tk.Button(gui, 
                  text ="Prepara registrazione", 
                  width=15, 
                  height=5, 
                  bg="#9933ff", 
                  fg="white", 
                  command = lambda: inizializzazione()).grid(row=3, column=1, pady=20)
                
    gui.mainloop()

if __name__ == '__main__': 
    control_gui()