import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit {


  properties: Array<any> = [
    {
      "Id":1,
      "Name":"Adani Housing",
      "Type":"House",
      "Price":12000
    },
    {
      "Id":2,
      "Name":"Birla Housing",
      "Type":"House",
      "Price":12000
    },
    {
      "Id":3,
      "Name":"Reliance Housing",
      "Type":"House",
      "Price":12000
    },
    {
      "Id":4,
      "Name":"Ganesh Housing",
      "Type":"House",
      "Price":12000
    }
]

  constructor() { }

  ngOnInit(): void {
  }

}
