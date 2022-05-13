import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { IPropertyBase } from 'src/app/model/ipropertybase';


@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrls: ['./add-property.component.css']
})
export class AddPropertyComponent implements OnInit {

  @ViewChild('Form') addPropertyForm: NgForm;

  @ViewChild('formTabs') formTabs: TabsetComponent;
  //SellRent = '1';

  //tmpProp = {}


  propertyTypes: Array<string> = ['Housing', 'Apartment', 'Duplex']
  furnishTypes: Array<string> = ['Fully', 'Semi', 'Unfurnished']
  readytomove: Array<string> = ['East','West','South','North']

  propertyView: IPropertyBase = {

    Id: null,
    Name: '',
    Price: null,
    SellRent: null,
    PType: null,
    FType: null,
    BHK: null,
    BuiltArea: null,
    City: null,
    RTM: null,

  };

  constructor(private router: Router) { }

  ngOnInit() {

    // //this.addPropertyForm.controls['Name'].setValue('Default Value');

    // setTimeout(()=> {
    //   this.addPropertyForm.controls['Name'].setValue('Default Value');
    // });
  }

  onBack(){
    this.router.navigate(['/']);
  }

  onSubmit()
  {
    console.log('Congrats, form Submitted');
    console.log('SellRent=' +  this.addPropertyForm.value.BasicInfo.SellRent);
    console.log(this.addPropertyForm);
  }

  selectTab(tabId: number) {
    this.formTabs.tabs[tabId].active = true;
  }
}
