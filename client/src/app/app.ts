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
    this.getUsers();
  }

  getUsers()
  {
     
    this.http.get('http://localhost:5091/api/user').subscribe(response => {this.users = response} , error => {console.log(error);})
  }
}
