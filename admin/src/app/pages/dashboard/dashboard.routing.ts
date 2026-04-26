import { Routes } from "@angular/router";
import { ActionTypes } from "src/app/core/models/fixed-value";
import { DashboardComponent } from "./dashboard.component";

export const routes:Routes=[
    {path:'',component:DashboardComponent,
    data: {
        pageAction:ActionTypes.LIST,
        component:"DashboardComponent"
    },
}
]