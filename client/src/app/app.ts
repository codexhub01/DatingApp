import { HttpClient } from '@angular/common/http';
import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  imports: [RouterOutlet],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App implements OnInit {
  protected readonly title = 'New Title';
  
  users : any;

  constructor(private http : HttpClient){}


  ngOnInit() {
    
    this.http.get('https://localhost:5001/api/users').subscribe()
  }
}
