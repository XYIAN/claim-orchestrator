// Enhanced site.js with advanced animations and loading states

// Global animation controller
const AnimationController = {
  // Page transition management
  initPageTransitions() {
    const mainContent = document.querySelector('main');
    if (mainContent) {
      mainContent.classList.add('page-transition');

      // Trigger page load animation
      setTimeout(() => {
        mainContent.classList.add('loaded');
      }, 100);
    }
  },

  // Loading overlay management
  showLoadingOverlay(message = 'Loading...') {
    let overlay = document.getElementById('loading-overlay');
    if (!overlay) {
      overlay = document.createElement('div');
      overlay.id = 'loading-overlay';
      overlay.className = 'loading-overlay';
      overlay.innerHTML = `
                <div class="text-center">
                    <div class="loading-spinner mb-3"></div>
                    <p class="text-brown fw-semibold">${message}</p>
                </div>
            `;
      document.body.appendChild(overlay);
    }

    setTimeout(() => {
      overlay.classList.add('active');
    }, 10);
  },

  hideLoadingOverlay() {
    const overlay = document.getElementById('loading-overlay');
    if (overlay) {
      overlay.classList.remove('active');
      setTimeout(() => {
        overlay.remove();
      }, 300);
    }
  },

  // Skeleton loading for content
  showSkeletonLoading(container, rows = 3) {
    const skeleton = document.createElement('div');
    skeleton.className = 'table-skeleton';

    for (let i = 0; i < rows; i++) {
      const row = document.createElement('div');
      row.className = 'skeleton-row';

      for (let j = 0; j < 4; j++) {
        const cell = document.createElement('div');
        cell.className = 'skeleton-cell';
        row.appendChild(cell);
      }

      skeleton.appendChild(row);
    }

    container.innerHTML = '';
    container.appendChild(skeleton);
  },

  // Enhanced card animations
  animateCards() {
    const cards = document.querySelectorAll('.card');
    const observer = new IntersectionObserver(
      entries => {
        entries.forEach(entry => {
          if (entry.isIntersecting) {
            entry.target.style.opacity = '1';
            entry.target.style.transform = 'translateY(0)';
          }
        });
      },
      { threshold: 0.1 }
    );

    cards.forEach(card => {
      observer.observe(card);
    });
  },

  // Table row animations
  animateTableRows() {
    const tableRows = document.querySelectorAll('.table-hover tbody tr');
    const observer = new IntersectionObserver(
      entries => {
        entries.forEach(entry => {
          if (entry.isIntersecting) {
            entry.target.style.opacity = '1';
            entry.target.style.transform = 'translateX(0)';
          }
        });
      },
      { threshold: 0.1 }
    );

    tableRows.forEach(row => {
      observer.observe(row);
    });
  },

  // Enhanced button interactions
  enhanceButtons() {
    const buttons = document.querySelectorAll('.btn');

    buttons.forEach(button => {
      // Add ripple effect
      button.addEventListener('click', function (e) {
        const ripple = document.createElement('span');
        const rect = this.getBoundingClientRect();
        const size = Math.max(rect.width, rect.height);
        const x = e.clientX - rect.left - size / 2;
        const y = e.clientY - rect.top - size / 2;

        ripple.style.width = ripple.style.height = size + 'px';
        ripple.style.left = x + 'px';
        ripple.style.top = y + 'px';
        ripple.classList.add('ripple');

        this.appendChild(ripple);

        setTimeout(() => {
          ripple.remove();
        }, 600);
      });

      // Add loading state for submit buttons
      if (button.type === 'submit') {
        button.addEventListener('click', function () {
          if (!this.classList.contains('loading')) {
            this.classList.add('loading');
            this.disabled = true;

            // Store original text
            this.dataset.originalText = this.innerHTML;
            this.innerHTML = '<span class="loading-spinner-small"></span>Processing...';

            // Re-enable after a delay (in real app, this would be after AJAX completion)
            setTimeout(() => {
              this.classList.remove('loading');
              this.disabled = false;
              this.innerHTML = this.dataset.originalText;
            }, 2000);
          }
        });
      }
    });
  },

  // Enhanced form interactions
  enhanceForms() {
    const formControls = document.querySelectorAll('.form-control');

    formControls.forEach(control => {
      // Add focus animations
      control.addEventListener('focus', function () {
        this.parentElement.classList.add('focused');
        this.classList.add('bounce');
      });

      control.addEventListener('blur', function () {
        this.parentElement.classList.remove('focused');
        this.classList.remove('bounce');
      });

      // Add validation animations
      control.addEventListener('input', function () {
        if (this.checkValidity()) {
          this.classList.remove('shake');
          this.classList.add('pulse');
          setTimeout(() => this.classList.remove('pulse'), 1000);
        }
      });

      control.addEventListener('invalid', function (e) {
        e.preventDefault();
        this.classList.add('shake');
        setTimeout(() => this.classList.remove('shake'), 800);
      });
    });
  },

  // Enhanced table interactions
  enhanceTables() {
    const tableRows = document.querySelectorAll('.table-hover tbody tr');

    tableRows.forEach(row => {
      row.addEventListener('mouseenter', function () {
        this.style.transform = 'scale(1.02) translateX(5px)';
      });

      row.addEventListener('mouseleave', function () {
        this.style.transform = 'scale(1) translateX(0)';
      });

      // Add click animation
      row.addEventListener('click', function () {
        this.classList.add('pulse');
        setTimeout(() => this.classList.remove('pulse'), 1000);
      });
    });
  },

  // Smooth scrolling
  initSmoothScrolling() {
    const anchorLinks = document.querySelectorAll('a[href^="#"]');

    anchorLinks.forEach(link => {
      link.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
          target.scrollIntoView({
            behavior: 'smooth',
            block: 'start',
          });
        }
      });
    });
  },

  // Enhanced alerts
  enhanceAlerts() {
    const alerts = document.querySelectorAll('.alert');

    alerts.forEach(alert => {
      // Add entrance animation
      alert.classList.add('fade-in-up');

      // Auto-dismiss with fade out
      setTimeout(() => {
        alert.style.transition = 'all 0.5s cubic-bezier(0.4, 0, 0.2, 1)';
        alert.style.opacity = '0';
        alert.style.transform = 'translateY(-20px)';
        setTimeout(() => {
          alert.remove();
        }, 500);
      }, 5000);
    });
  },

  // Enhanced badges
  enhanceBadges() {
    const badges = document.querySelectorAll('.badge');

    badges.forEach(badge => {
      badge.addEventListener('mouseenter', function () {
        this.style.transform = 'scale(1.1) translateY(-2px)';
      });

      badge.addEventListener('mouseleave', function () {
        this.style.transform = 'scale(1) translateY(0)';
      });

      // Add click animation
      badge.addEventListener('click', function () {
        this.classList.add('bounce');
        setTimeout(() => this.classList.remove('bounce'), 1000);
      });
    });
  },

  // Enhanced navigation
  enhanceNavigation() {
    const navLinks = document.querySelectorAll('.navbar-nav .nav-link');

    navLinks.forEach(link => {
      link.addEventListener('click', function () {
        // Add click animation
        this.classList.add('pulse');
        setTimeout(() => this.classList.remove('pulse'), 1000);
      });
    });
  },

  // Progress bar animations
  animateProgressBars() {
    const progressBars = document.querySelectorAll('.progress-bar');

    const observer = new IntersectionObserver(
      entries => {
        entries.forEach(entry => {
          if (entry.isIntersecting) {
            const progressBar = entry.target;
            const width = progressBar.style.width || '0%';
            progressBar.style.width = '0%';

            setTimeout(() => {
              progressBar.style.width = width;
            }, 100);
          }
        });
      },
      { threshold: 0.5 }
    );

    progressBars.forEach(bar => {
      observer.observe(bar);
    });
  },

  // Parallax effect for backgrounds
  initParallax() {
    window.addEventListener('scroll', () => {
      const scrolled = window.pageYOffset;
      const parallaxElements = document.querySelectorAll('.parallax-bg');

      parallaxElements.forEach(element => {
        const speed = element.dataset.speed || 0.5;
        element.style.transform = `translateY(${scrolled * speed}px)`;
      });
    });
  },

  // Initialize all animations
  init() {
    this.initPageTransitions();
    this.animateCards();
    this.animateTableRows();
    this.enhanceButtons();
    this.enhanceForms();
    this.enhanceTables();
    this.initSmoothScrolling();
    this.enhanceAlerts();
    this.enhanceBadges();
    this.enhanceNavigation();
    this.animateProgressBars();
    this.initParallax();
  },
};

// Initialize animations when DOM is loaded
document.addEventListener('DOMContentLoaded', function () {
  AnimationController.init();
});

// Add CSS for enhanced animations
const enhancedStyles = document.createElement('style');
enhancedStyles.textContent = `
    .btn {
        position: relative;
        overflow: hidden;
    }
    
    .ripple {
        position: absolute;
        border-radius: 50%;
        background: rgba(255, 255, 255, 0.3);
        transform: scale(0);
        animation: ripple-animation 0.6s linear;
        pointer-events: none;
    }
    
    @keyframes ripple-animation {
        to {
            transform: scale(4);
            opacity: 0;
        }
    }
    
    .focused .form-control {
        border-color: #8B7355;
        box-shadow: 0 0 0 0.2rem rgba(139, 115, 85, 0.25);
    }

    .loading-spinner-small {
        width: 16px;
        height: 16px;
        border: 2px solid rgba(139, 115, 85, 0.2);
        border-top: 2px solid #8B7355;
        border-radius: 50%;
        animation: spin 1s linear infinite;
        display: inline-block;
        margin-right: 8px;
    }

    .table-skeleton {
        background: rgba(255, 255, 255, 0.8);
        border-radius: 8px;
        padding: 1rem;
        margin-bottom: 1rem;
    }

    .table-skeleton .skeleton-row {
        display: flex;
        margin-bottom: 0.5rem;
    }

    .table-skeleton .skeleton-cell {
        flex: 1;
        height: 20px;
        margin-right: 1rem;
        background: linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%);
        background-size: 200% 100%;
        animation: skeleton-loading 1.5s infinite;
        border-radius: 4px;
    }

    @keyframes skeleton-loading {
        0% { background-position: 200% 0; }
        100% { background-position: -200% 0; }
    }
`;
document.head.appendChild(enhancedStyles);

// Enhanced utility functions
window.ClaimOrchestrator = {
  // Show loading state with custom message
  showLoading: function (element, message = 'Loading...') {
    if (typeof element === 'string') {
      element = document.querySelector(element);
    }

    if (element) {
      element.classList.add('loading');
      element.disabled = true;

      // Store original content
      element.dataset.originalContent = element.innerHTML;
      element.innerHTML = `<span class="loading-spinner-small"></span>${message}`;
    }
  },

  // Hide loading state
  hideLoading: function (element) {
    if (typeof element === 'string') {
      element = document.querySelector(element);
    }

    if (element) {
      element.classList.remove('loading');
      element.disabled = false;
      element.innerHTML = element.dataset.originalContent || element.innerHTML;
    }
  },

  // Show success message with enhanced animation
  showSuccess: function (message, duration = 5000) {
    const alert = document.createElement('div');
    alert.className = 'alert alert-success alert-dismissible fade show';
    alert.innerHTML = `
            <i class="fas fa-check-circle me-2"></i>
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;

    const container =
      document.querySelector('.container-fluid') || document.querySelector('.container');
    container.insertBefore(alert, container.firstChild);

    // Add entrance animation
    alert.classList.add('fade-in-up');

    // Auto-dismiss after specified duration
    setTimeout(() => {
      alert.style.transition = 'all 0.5s cubic-bezier(0.4, 0, 0.2, 1)';
      alert.style.opacity = '0';
      alert.style.transform = 'translateY(-20px)';
      setTimeout(() => alert.remove(), 500);
    }, duration);
  },

  // Show error message with enhanced animation
  showError: function (message, duration = 8000) {
    const alert = document.createElement('div');
    alert.className = 'alert alert-danger alert-dismissible fade show';
    alert.innerHTML = `
            <i class="fas fa-exclamation-triangle me-2"></i>
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;

    const container =
      document.querySelector('.container-fluid') || document.querySelector('.container');
    container.insertBefore(alert, container.firstChild);

    // Add entrance animation
    alert.classList.add('fade-in-up');

    // Auto-dismiss after specified duration
    setTimeout(() => {
      alert.style.transition = 'all 0.5s cubic-bezier(0.4, 0, 0.2, 1)';
      alert.style.opacity = '0';
      alert.style.transform = 'translateY(-20px)';
      setTimeout(() => alert.remove(), 500);
    }, duration);
  },

  // Show warning message
  showWarning: function (message, duration = 6000) {
    const alert = document.createElement('div');
    alert.className = 'alert alert-warning alert-dismissible fade show';
    alert.innerHTML = `
            <i class="fas fa-exclamation-circle me-2"></i>
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;

    const container =
      document.querySelector('.container-fluid') || document.querySelector('.container');
    container.insertBefore(alert, container.firstChild);

    // Add entrance animation
    alert.classList.add('fade-in-up');

    // Auto-dismiss after specified duration
    setTimeout(() => {
      alert.style.transition = 'all 0.5s cubic-bezier(0.4, 0, 0.2, 1)';
      alert.style.opacity = '0';
      alert.style.transform = 'translateY(-20px)';
      setTimeout(() => alert.remove(), 500);
    }, duration);
  },

  // Show info message
  showInfo: function (message, duration = 4000) {
    const alert = document.createElement('div');
    alert.className = 'alert alert-info alert-dismissible fade show';
    alert.innerHTML = `
            <i class="fas fa-info-circle me-2"></i>
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;

    const container =
      document.querySelector('.container-fluid') || document.querySelector('.container');
    container.insertBefore(alert, container.firstChild);

    // Add entrance animation
    alert.classList.add('fade-in-up');

    // Auto-dismiss after specified duration
    setTimeout(() => {
      alert.style.transition = 'all 0.5s cubic-bezier(0.4, 0, 0.2, 1)';
      alert.style.opacity = '0';
      alert.style.transform = 'translateY(-20px)';
      setTimeout(() => alert.remove(), 500);
    }, duration);
  },

  // Show loading overlay
  showLoadingOverlay: function (message = 'Loading...') {
    AnimationController.showLoadingOverlay(message);
  },

  // Hide loading overlay
  hideLoadingOverlay: function () {
    AnimationController.hideLoadingOverlay();
  },

  // Show skeleton loading
  showSkeletonLoading: function (container, rows = 3) {
    const targetContainer =
      typeof container === 'string' ? document.querySelector(container) : container;
    if (targetContainer) {
      AnimationController.showSkeletonLoading(targetContainer, rows);
    }
  },

  // Animate element with specified animation
  animate: function (element, animationClass, duration = 1000) {
    const targetElement = typeof element === 'string' ? document.querySelector(element) : element;
    if (targetElement) {
      targetElement.classList.add(animationClass);
      setTimeout(() => {
        targetElement.classList.remove(animationClass);
      }, duration);
    }
  },
};
