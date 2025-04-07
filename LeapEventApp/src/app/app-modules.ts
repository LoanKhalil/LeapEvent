import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FlexLayoutModule } from "@ngbracket/ngx-layout";
import { MaterialComponentsModule } from "./material-components.module";
import { RouterModule } from "@angular/router";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        ReactiveFormsModule,
        MaterialComponentsModule,
        FlexLayoutModule
        
      ],
      exports: [
        CommonModule,
        FormsModule,
        RouterModule,
        ReactiveFormsModule,
        MaterialComponentsModule,
        FlexLayoutModule
      ],
})
export class AppModules{

}