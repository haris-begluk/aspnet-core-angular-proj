import { ToastrService } from 'ngx-toastr';
import {ErrorHandler, Inject, NgZone} from '@angular/core'; 
export class AppErrorHandler implements ErrorHandler { 
    constructor(private ngZone: NgZone,@Inject(ToastrService) private toastr:ToastrService){

    }
    handleError(error: any):void{
        // throw new Error('Method not implemented!');  
        this.ngZone.run(() => {
            console.log('ERROR'); 
            this.toastr.error('An error happend!', "Error", {timeOut:5000});
        });
        
   
    }
} 
// var Zone = {
//     run: function (callback){ 
//         console.log("BEFORE"); 
//         if(this.beforeTask) 
//         this.beforeTask();
//         callback();  
//         if(this.afterTask) 
//         this.afterTask();
//         console.log("AFTER");
//     }
// }  
// Zone.beforeTask = () => {console.log("Before!")}; 
// Zone.afterTask = () => {console.log("After!")};
// Zone.run(() =>{console.log("Hello World")}); 

// var _setTimeout = setTimeout; 
// setTimeout = (callback, timeout) => {
//     Zone.run(()=>{
//         _setTimeout(callback, timeout);
//     });
// } 
// setTimeout(() => console.log("SETTIMEOUT"));