import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [FormsModule],
  selector: 'app-nav',
  styleUrl: './nav.css',
  templateUrl: './nav.html',
})
export class Nav implements OnInit {

  model : any = {}
  constructor()
  {

  }

  ngOnInit(): void {
    
  }

  login()
  {
    console.log(this.model);
  }
}
