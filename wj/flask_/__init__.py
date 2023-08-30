from flask import Flask, jsonify, request
import speech_recognition as sr
from flask_cors import CORS

app = Flask(__name__)
CORS(app)  # CORS 처리

@app.route('/', methods=['GET','POST'])
def test():
    r = sr.Recognizer()

    if request.method == 'POST':

        # 보이스 데이터 받기
        print(request.files['file'])
        print(request.files['file'].filename)
        audio = request.files['file']

        path = 'test.wav'  # 음성파일 경로설정
        audio.save(path)

        with sr.AudioFile(path) as source:
            audio = r.record(source)
            result = r.recognize_google(audio, language='ko-KR')
            print('음성: ' + result)


    return jsonify({"message": result})

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)