//Date and number format locale
function formatLocale() {
    $('.date').datepicker({language: 'sl', autoclose: true, format: 'dd.mm.yyyy'});
    $('.money').inputmask('decimal', {radixPoint: ',', autoGroup: true, groupSeparator: '.', digits: 2, groupSize: 3, allowMinus: true});
    $('.money').blur(jQuery('.money').formatThousand);
}

//Posivim ekran ko delam
function grayOut(vis) {
var options = options || {};
var zindex = options.zindex || 50;
var opacity = options.opacity || 60;
var opaque = opacity / 100;
var bgcolor = options.bgcolor || '#FFFFFF';
var dark=document.getElementById('darkenScreenObject');


if (!dark) {
var tbody = document.getElementsByTagName("body")[0];
var tnode = document.createElement('div'); // Create the layer.
var img   = document.createElement('img');
var box   = document.createElement('div');

tnode.style.position='absolute'; // Position absolutely
tnode.style.top='0px'; // In the top
tnode.style.left='0px'; // Left corner of the page
tnode.style.overflow='hidden'; // Try to avoid making scroll bars
tnode.style.display='none'; // Start out Hidden
tnode.id='darkenScreenObject'; // Name it so we can find it later

box.className='centered';

box.appendChild(img);
tnode.appendChild(box);
tbody.appendChild(tnode); // Add it to the web page
dark=document.getElementById('darkenScreenObject'); // Get the object.
}
if (vis) {
  document.body.style.cursor="wait";
  // Calculate the page width and height
  if( document.body && ( document.body.scrollWidth || document.body.scrollHeight ) ) {
    var pageWidth = document.body.scrollWidth+'px';
    var pageHeight = document.body.scrollHeight+'px';
    } else if( document.body.offsetWidth ) {
    var pageWidth = document.body.offsetWidth+'px';
    var pageHeight = document.body.offsetHeight+'px';
    } else {
    var pageWidth='100%';
    var pageHeight='100%';
  }
  //set the shader to cover the entire page and make it visible.
  dark.style.opacity=opaque;
  dark.style.MozOpacity=opaque;
  dark.style.filter='alpha(opacity='+opacity+')';
  dark.style.zIndex=zindex;
  dark.style.backgroundColor=bgcolor;
  dark.style.width= pageWidth;
  dark.style.height= pageHeight;
  dark.style.display='block';
} else {
  document.body.style.cursor= "auto";
  dark.style.display='none';
}
}

//Cookies util
function setCookie(c_name,value,exdays)
{
    var exdate=new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + (exdays === null) ? "" : "; expires=" + exdate.toUTCString();
    document.cookie=c_name + "=" + c_value;
}

function getCookie(c_name)
{
    var i,x,y,ARRcookies=document.cookie.split(";");
    for (i=0; i<ARRcookies.length; i++)
    {
        x=ARRcookies[i].substr(0,ARRcookies[i].indexOf("="));
        y=ARRcookies[i].substr(ARRcookies[i].indexOf("=")+1);
        x=x.replace(/^\s+|\s+$/g,"");
        if (x===c_name)
        {
            return unescape(y);
        }
    }
}

function getCookies(){
  var pairs = document.cookie.split(";");
  var cookies = {};
  for (var i=0; i<pairs.length; i++){
    var pair = pairs[i].split("=");
    cookies[pair[0]] = unescape(pair[1]);
  }
  return cookies;
}

//Get sortClass
function getSortClass(asc) {
    if (asc) {
        return 'glyphicon glyphicon-triangle-top';
    } else {
        return 'glyphicon glyphicon-triangle-bottom';
    }
}