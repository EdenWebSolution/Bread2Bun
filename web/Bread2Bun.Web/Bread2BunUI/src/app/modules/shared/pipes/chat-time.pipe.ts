import { Pipe, PipeTransform, NgZone } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs/Observer';

interface processOutput {
  text: string; // Convert timestamp to string
  timeToUpdate: number; // Time until update in milliseconds
}

@Pipe({
  name: 'chatTime',
  pure: true
})
export class ChatTimePipe implements PipeTransform {

  constructor(private ngZone: NgZone) { }

  private process = (timestamp: number): processOutput => {
    let text: string;
    let timeToUpdate: number;

    const now = new Date();

    // Time ago in milliseconds
    const timeAgo: number = now.getTime() - timestamp;

    const seconds = timeAgo / 1000;
    const minutes = seconds / 60;
    const hours = minutes / 60;
    const days = hours / 24;
    const months = days / 30.416;
    const years = days / 365;

    if (seconds <= 10) {
      text = 'Just now';
    } else if (seconds <= 45) {
      text = 'a few seconds ago';
    } else if (seconds <= 90) {
      text = 'a minute ago';
    } else if (minutes <= 50) {
      text = Math.round(minutes) + ' minutes ago';
    } else if (hours <= 1.5) {
      text = 'an hour ago';
    } else if (hours <= 22) {
      text = Math.round(hours) + ' hours ago';
    } else if (hours <= 36) {
      text = 'a day ago';
    } else if (days <= 25) {
      text = Math.round(days) + ' days ago';
    } else if (months <= 1.5) {
      text = 'a month ago';
    } else if (months <= 11.5) {
      text = Math.round(months) + ' months ago';
    } else if (years <= 1.5) {
      text = 'a year ago';
    } else {
      text = Math.round(years) + ' years ago';
    }

    if (minutes < 1) {
      // update every 2 secs
      timeToUpdate = 2 * 1000;
    } else if (hours < 1) {
      // update every 30 secs
      timeToUpdate = 30 * 1000;
    } else if (days < 1) {
      // update every 5 mins
      timeToUpdate = 300 * 1000;
    } else {
      // update every hour
      timeToUpdate = 3600 * 1000;
    }

    return {
      text,
      timeToUpdate
    };
  }

  public transform = (value: string | Date): Observable<string> => {
    let d: Date;
    if (value instanceof Date) {
      d = value;
    }
    else {
      d = new Date(value);
    }
    // time value in milliseconds
    const timestamp = d.getTime();

    let timeoutID;

    return Observable.create((observer: Observer<string>) => {
      let latestText = '';

      // Repeatedly set new timeouts for new update checks.
      const registerUpdate = () => {
        const processOutput = this.process(timestamp);
        if (processOutput.text !== latestText) {
          latestText = processOutput.text;
          this.ngZone.run(() => {
            observer.next(latestText);
          });
        }
        timeoutID = setTimeout(registerUpdate, processOutput.timeToUpdate);
      };

      this.ngZone.runOutsideAngular(registerUpdate);

      // Return teardown function
      const teardownFunction = () => {
        if (timeoutID) {
          clearTimeout(timeoutID);
        }
      };
      return teardownFunction;
    });
  }

}
