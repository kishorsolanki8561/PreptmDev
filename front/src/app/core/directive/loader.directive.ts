import { ComponentRef, Directive, ElementRef, HostBinding, Input, Renderer2, ViewContainerRef } from '@angular/core';
import { LoaderComponent } from '../components/loader/loader.component';

@Directive({
    selector: '[loading]',
    standalone: true
})
export class LoaderDirective {
    private cmpRef: ComponentRef<LoaderComponent>;
    @HostBinding('class.relative') isVisible = false;
    @HostBinding('style.min-height') height = '50px';
    constructor(
        private readonly el: ElementRef,
        private readonly vcr: ViewContainerRef,
        private readonly renderer: Renderer2
    ) { }

    @Input() set loading(showLoader: boolean) {
        if (showLoader) {
            this.showComponent();
        } else {
            this.hideComponent();
        }
    }

    @Input() set ignoreMinHeight(val: boolean) {
        if (val) {
            this.height = '0px'
        }
    }


    private get contentEl(): HTMLElement {
        return this.el.nativeElement;
    }

    private get loaderEl(): HTMLElement {
        return this.cmpRef.location.nativeElement;
    }

    private get loaderViewIdx(): number {
        return this.vcr.indexOf(this.cmpRef.hostView);
    }

    private showComponent(): void {
        if (this.isVisible) {
            return;
        }

        this.createComponent();
        this.changeLoaderElParent();
        this.isVisible = true;
    }


    private hideComponent(): void {
        if (!this.isVisible) {
            return;
        }
        this.removeComponent();
        this.isVisible = false;
    }

    private changeLoaderElParent(): void {
        this.renderer.appendChild(this.contentEl, this.loaderEl);
    }
    private removeComponent(): void {
        this.vcr.remove(this.loaderViewIdx);
    }
    private createComponent(): void {
        this.cmpRef = this.vcr.createComponent(LoaderComponent);
    }

}
