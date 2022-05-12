import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor() { }

  authUser(user: any){

    let UserArray = [];
    if (localStorage.getItem('Users')) {
      UserArray = JSON.parse(localStorage.getItem('Users') as string);
    }
    //return UserArray.find(p => p.userName === user.userName && p.psaaword === user.psaaword);
    return UserArray.find((p: { userName: any; psaaword: any; }) => p.userName === user.userName && p.psaaword === user.psaaword);

  }
}
