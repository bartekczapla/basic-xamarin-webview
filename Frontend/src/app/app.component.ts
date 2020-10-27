import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Frontend';
  log: string;
  value: string;
  sent: boolean;
  ngOnInit(): void {
    (window as any).helloworld = function helloworld() {
      this.log = 'helloworld';
    };
  }

  testAlert(): void {
    window.alert('test');
  }

  onChange() {
    this.sent = false;
  }

  sendValue(): void {
    this.sent = false;
    var customWindow = window as any;
    if (customWindow.getAnimals) {
      const value = (window as any).getAnimals(this.value);
      this.sent = true;
      this.log = JSON.stringify(value);
    }
  }
}
