from typing import List
from flask import Flask, jsonify, request
import time
import fsdb

app = Flask('maxiserver')


@app.route('/send-event', methods=['POST'])
def send_event():
    time.sleep(2)#For demonstarion events sending 
    try:
        received_data: dict = request.get_json(silent=True)
        if received_data is None:
            raise Exception("request body not presented")

        events: List[dict] = received_data.get('events', None)
        if events is None:
            raise Exception("events field not presented")

        fsdb.add(events)
    except Exception as e:
        return __send_error(f"{str(e)}", 400)

    return __send_ok(f"{len(events)} events saved")


@app.route('/get-all', methods=['GET'])
def get_all():
    try:
        events = fsdb.read_all()
    except Exception as e:
        return __send_error(str(e))

    return jsonify(events), 200


@app.route('/reset', methods=['GET'])
def reset():
    try:
        fsdb.delete_all()
    except Exception as e:
        return __send_error(str(e))

    return __send_ok("all data removed")


def __send_ok(message: str) -> tuple:
    return jsonify({
        'error': False,
        'message': message
    }), 200


def __send_error(message: str, status_code: int = 500) -> tuple:
    return jsonify({
        'error': True,
        'message': message
    }), status_code


if __name__ == '__main__':
    fsdb.init()
    app.run('0.0.0.0', 5000)
