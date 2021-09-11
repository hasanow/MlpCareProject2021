import { Injectable } from '@angular/core';

declare let alertify: any;

@Injectable()
export default class AlertifyService {

  Dogrulama(mesaj: string, tamamIslem: any, hayirIslem: any, tamamMetin: string = "Tamam", iptalMetin: string = "İptal") {

    alertify.confirm("İşlem Onayı", mesaj, function () { tamamIslem() }, function () { hayirIslem() }).set("labels", { ok: tamamMetin, cancel: iptalMetin });

    alertify.confirm(mesaj,
      function () { tamamIslem() },
      function () { hayirIslem() }
    );
  }
  Alert(mesaj: string, islem?: any, baslik?: string) {
    alertify.alert(baslik || "Uyarı", mesaj, function () { if (islem) islem() });
  }
  BasariliIslem(mesaj: string) {
    alertify.set('notifier', 'position', 'top-right');
    alertify.success(mesaj);
  }
  HataliIslem(mesaj: string) {
    alertify.set('notifier', 'position', 'top-right');
    alertify.error(mesaj);
  }
  Uyari(mesaj: string) {
    alertify.set('notifier', 'position', 'top-right');
    alertify.warning(mesaj);
  }

}
