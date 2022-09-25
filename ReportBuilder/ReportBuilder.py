import os
import json
import shutil
import pandas as pd

from flask import Flask, request, jsonify, send_from_directory, wraps


app = Flask(__name__)


@app.route('/', methods=['GET'])
def index():
    return 'Hello World'


def clean_output(f):
    @wraps(f)
    def function(*args, **kwargs):
        shutil.rmtree('./output', ignore_errors=True)
        return f(*args, **kwargs)
    return function


@app.route('/create', methods=['POST'])
@clean_output
def create_report():

    output_path = './output'

    os.makedirs(output_path, exist_ok=True)

    data = request.args
    df = pd.DataFrame(['hello', 'world'])
    df.to_excel(os.path.join(output_path, 'test.xlsx'))
    
    return send_from_directory(
        directory=output_path,
        path='test.xlsx',
        as_attachment=True
    )





app.run()

if __name__ == '__main__':
    app.run()
