// Auto update layout
//(function() {
//  window.layoutHelpers.setAutoUpdate(true);
//})();

// Collapse menu
(function() {
  if ($('#layout-sidenav').hasClass('sidenav-horizontal') || window.layoutHelpers.isSmallScreen()) {
    return;
  }

  try {
    window.layoutHelpers.setCollapsed(
      localStorage.getItem('layoutCollapsed') === 'true',
      false
    );
  } catch (e) {}
})();

$(function() {
  // Initialize sidenav
  $('#layout-sidenav').each(function() {
    new SideNav(this, {
      orientation: $(this).hasClass('sidenav-horizontal') ? 'horizontal' : 'vertical'
    });
  });

  // Initialize sidenav togglers
  $('body').on('click', '.layout-sidenav-toggle', function(e) {
    e.preventDefault();
    window.layoutHelpers.toggleCollapsed();
    if (!window.layoutHelpers.isSmallScreen()) {
      try { localStorage.setItem('layoutCollapsed', String(window.layoutHelpers.isCollapsed())); } catch (e) {}
    }
  });

  if ($('html').attr('dir') === 'rtl') {
    $('#layout-navbar .dropdown-menu').toggleClass('dropdown-menu-right');
  }

 //for tabs...Abdulkadir Ceyhan personal solution...
 //https ://mdbootstrap.com/support/tab-panel-switch-doesnt-work/

  $(".nav-tabs a.nav-link").click(function () {
      let x = $(this).attr("href");
      x = x.replace("#", "");
      $(".tab-content .tab-pane").each(function () {
          var y = $(this).attr("id");
          if (x == y) $(this).addClass("active show");
          else $(this).removeClass("active show");
      });
  });


});


