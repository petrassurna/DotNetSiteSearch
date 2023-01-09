

document.addEventListener("DOMContentLoaded", function () {


    DNS_SetZIndexes();

    document.getElementById('site-search').onclick = function () {

        document.getElementById('dns-search-bar').classList.add('clicked');
        document.getElementById('dns-search-overlay').style.display = "block";
        document.getElementById("dns-search-input").focus();

    }


    document.getElementById('dns-search-close').onclick = function () {
        DNS_CloseSearch();
    }

    document.getElementById("dns-search-input").addEventListener("keydown", function (event) {
        if (event.key === "Enter") {
            DNS_debounceSearch();
        }
    });

    var DNS_debounceSearch = DNS_Debounce(DNS_GetSearchResults, 400);
    document.getElementById("dns-search-input").addEventListener("keyup", function (event) {

        if (this.value.length > 3) {
            DNS_debounceSearch();
        }

    });


    var dns_search_results = document.getElementById('dns-search-results');
    dns_search_results.addEventListener('scroll', function () {

        if (dns_search_results.scrollTop + dns_search_results.clientHeight >= dns_search_results.scrollHeight - 5) {

            if(document.getElementById('end') == null)
            {
                DNS_GetSearchResultsAfterScroll();
            }

        }
    });

});



document.onkeydown = function (evt) {
    evt = evt || window.event;

    if (evt.key == 'Escape' && DNS_TextInputIsActive()) {
        DNS_CloseSearch();
    }
};


function DNS_ClearSearchResults() {

    const elements = document.querySelectorAll('.dns-search-result');
    elements.forEach(element => element.remove());

    document.getElementById('dns-search-results').style.overflowY = 'hidden';
}


function DNS_CloseSearch() {
    DNS_ClearSearchResults();
    document.getElementById("dns-search-input").value = '';
    document.getElementById('dns-search-results').classList.remove('show');
    document.getElementById('dns-search-bar').classList.remove('clicked');
    document.getElementById('dns-search-overlay').style.display = 'none';
}


function DNS_CreateSearchResult(text, id) {
    var search_result = document.createElement('div');
    search_result.id = id;
    search_result.className = "dns-search-result";

    var paragraph = document.createElement('p');
    paragraph.innerHTML = 'End of results';
    search_result.appendChild(paragraph);

    return search_result;

}


function DNS_Debounce(func, wait, immediate) {

    var timeout;

    return function () {
        var context = this,
            args = arguments;

        var callNow = immediate && !timeout;

        clearTimeout(timeout);

        timeout = setTimeout(function () {

            timeout = null;

            if (!immediate) {
                func.apply(context, args);
            }
        }, wait);

        if (callNow) func.apply(context, args);
    }
}


function DNS_DeleteSearchResults() {
    const noResults = document.getElementById('no-results');
    if (noResults != null) {
        const parent = noResults.parentNode;
        parent.removeChild(noResults);
    }
}


function DNS_DrawNoResults(searchTerm) {

    var noResults = document.getElementById('no-results');
    var search_results = document.getElementById('dns-search-results');

    if (noResults == null) {

        var search_result = document.createElement('div');
        search_result.setAttribute("id", "no-results");

        search_result.className = "dns-search-result no-result";

        var paragraph = document.createElement('p');
        paragraph.innerHTML = 'There are no results for the phrase \'' + searchTerm + '\'';
        search_result.appendChild(paragraph);

        search_results.appendChild(search_result);
        search_results.classList.add('show');

    }
}


function DNS_DrawSearchResult(data) {

    var path = data.path;
    var title = data.title;

    document.getElementById('dns-search-results').style.overflowY = 'scroll';

    var search_result = document.createElement('div');
    search_result.className = "dns-search-result";

    var heading = document.createElement('a');
    heading.className = "dns-heading";
    heading.innerHTML = data.title;
    heading.setAttribute('href', data.path);

    var link = document.createElement('a');
    link.innerText = data.path;
    link.setAttribute('href', data.path);

    search_result.appendChild(heading);

    for (var i = 0; i < data.wordMatches.length; i++) {
        var paragraph = document.createElement('p');
        paragraph.innerHTML = data.wordMatches[i];
        search_result.appendChild(paragraph);
    }

    search_result.appendChild(link);
    return search_result;
}


function DNS_DrawSearchResults(data, searchTerm) {

    var search_results = document.getElementById('dns-search-results');

    if (data.length > 0) {

        DNS_DeleteSearchResults();
        DNS_DrawSearchResultsDirect(data);
        DNS_SetSkip(DNS_GetSkip() + data.length);

    }
    else {

        DNS_DrawNoResults(searchTerm);

    }
}


function DNS_DrawSearchResultsAfterScroll(data, searchTerm) {

    var search_results = document.getElementById('dns-search-results');

    DNS_DrawSearchResultsDirect(data);
    DNS_SetSkip(DNS_GetSkip() + data.length);
}


function DNS_DrawSearchResultsDirect(data) {

    var search_results = document.getElementById('dns-search-results');
    search_results.classList.add('show');

    for (var i = 0; i < data.length; i++) {

        var result = DNS_DrawSearchResult(data[i]);
        search_results.appendChild(result);

    }
}


function DNS_FindHighestZIndex() {

    const elements = document.querySelectorAll('*');

    const zIndices = [].map.call(elements, function (el) {
        const zIndex = window.getComputedStyle(el).zIndex;
        return isNaN(zIndex) ? 0 : zIndex;
    });

    const highestZIndex = Math.max.apply(null, zIndices);

    return highestZIndex;
}


function DNS_GetSearchResults() {

    var element = document.getElementById('end');
    if(element != null) {
        element.remove();
    }

    DNS_ClearSearchResults();
    DNS_ShowLoader();

    var searchTerm = document.getElementById('dns-search-input').value;

    fetch("/Umbraco/Api/USiteSearch/Results?query=" + searchTerm + "&skip=0", {
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then((response) => {
            if (!response.ok) {
                throw Error(response.statusText);
            }
            return response;
        })
        .then((response) => response.json())
        .then((data) => {
            DNS_DrawSearchResults(data, searchTerm);
            DNS_HideLoader();
        })
        .catch((error) => {
            console.error(error);
        });

}


function DNS_GetSearchResultsAfterScroll() {

    document.getElementById('dns-search-skip').style.display = 'block';

    var searchTerm = document.getElementById('dns-search-input').value;

    skip = DNS_GetSkip();

    fetch("/Umbraco/Api/DNSSearch/Results?query=" + searchTerm + "&skip=" + skip, {
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then((response) => {
            if (!response.ok) {
                throw Error(response.statusText);
            }
            return response;
        })
        .then((response) => response.json())
        .then((data) => {

            if (data.length > 0) {
                DNS_DrawSearchResultsAfterScroll(data);
            }
            else if (  document.getElementById('end') == null ) {
                document
                  .getElementById('dns-search-results')
                  .appendChild(DNS_CreateSearchResult('End of results', 'end'));
            }

            document.getElementById('dns-search-skip').style.display = 'none';
        })
        .catch((error) => {
            console.error(error);
        });

}

function DNS_GetSkip() {
    var skip = 0;

    var skipAttribute = document.getElementById('dns-search-results').getAttribute('skip');

    if (skipAttribute != null) {
        skip = parseInt(skipAttribute);
    }

    return skip;
}


function DNS_HideLoader() {
    document.getElementById('dns-search-loader').style.display = 'none';
}


function DNS_SetSkip(skip) {
    return document.getElementById('dns-search-results').setAttribute('skip', skip);
}


function DNS_SetZIndexes() {
    var zIndex = DNS_FindHighestZIndex();

    document.getElementById('dns-search-bar').style.zIndex = zIndex + 2;
    document.getElementById('dns-search-overlay').style.zIndex = zIndex + 1;
}


function DNS_ShowLoader() {
    document.getElementById('dns-search-loader').style.display = 'block';
}


function DNS_TextInputIsActive() {
    var searchInput = document.getElementById('dns-search-input');
    return searchInput === document.activeElement;
}
